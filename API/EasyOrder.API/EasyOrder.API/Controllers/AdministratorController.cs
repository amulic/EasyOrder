using AutoMapper;
using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : Controller
    {
        private IAdministratorRepository _administratorRepository;
        private IMapper _mapper;

        public AdministratorController(IAdministratorRepository administratorRepository, IMapper mapper)  
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
        }

        [HttpGet("{administratorId}")]
        [ProducesResponseType(200, Type = typeof(Administrator))]
        [ProducesResponseType(400)]
        public IActionResult GetAdministrator(int adminId)
        {
            if (!_administratorRepository.AdministratorExists(adminId))
                return NotFound();

            var administrator = _mapper.Map<AdministratorDto>(_administratorRepository.GetAdministrator(adminId));
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(administrator);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Administrator))]
        public IActionResult GetAdministrators()
        {
            var administrators = _mapper.Map<List<AdministratorDto>>(_administratorRepository.GetAdministrators());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  

            return Ok(administrators);  
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAdministrator([FromBody]AdministratorDto administratorCreate)
        {
            if (administratorCreate == null)
                return BadRequest();

            var administrator = _administratorRepository.GetAdministrators().Where(a=>a.Id==administratorCreate.Id).FirstOrDefault();

            if(administrator != null)
            {
                ModelState.AddModelError("", "Administrator already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var administratorMap = _mapper.Map<Administrator>(administratorCreate);

            if(!_administratorRepository.CreateAdministrator(administratorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");

            
        }
    }
}
