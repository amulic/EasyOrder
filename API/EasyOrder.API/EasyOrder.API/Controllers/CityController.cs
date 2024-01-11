using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using EasyOrder.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private ICityRepository _cityRepository;
        private IMapper _mapper;

        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            if (!_cityRepository.CityExists(cityId))
                return NotFound();

            var city = _mapper.Map<BillDto>(_cityRepository.GetCity(cityId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(city);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetCities()
        {
            var cities = _mapper.Map<List<BrandDto>>(_cityRepository.GetCities());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cities);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCity([FromBody] CityDto cityCreate)
        {
            if (cityCreate == null)
                return BadRequest();

            var city = _cityRepository.GetCities().Where(c => c.Id == cityCreate.Id).FirstOrDefault();

            if (city != null)
            {
                ModelState.AddModelError("", "City already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var cityMap = _mapper.Map<City>(cityCreate);

            if (!_cityRepository.CreateCity(cityMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
