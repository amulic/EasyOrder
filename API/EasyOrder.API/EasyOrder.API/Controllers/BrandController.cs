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
    public class BrandController : Controller
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet("{brandId}")]
        public IActionResult GetBrand(int brandId) 
        {
            if (!_brandRepository.BrandExists(brandId))
                return NotFound();

            var brand = _mapper.Map<BillDto>(_brandRepository.GetBrand(brandId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(brand);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetBrands()
        {
            var brands = _mapper.Map<List<BrandDto>>(_brandRepository.GetBrands());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(brands);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBrand([FromBody] BrandDto brandCreate)
        {
            if (brandCreate == null)
                return BadRequest();

            var brand = _brandRepository.GetBrands().Where(c => c.Id == brandCreate.Id).FirstOrDefault();

            if (brand != null)
            {
                ModelState.AddModelError("", "Brand already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var brandMap = _mapper.Map<Brand>(brandCreate);

            if (!_brandRepository.CreateBrand(brandMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
