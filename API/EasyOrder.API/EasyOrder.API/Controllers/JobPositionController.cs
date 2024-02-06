using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionController : Controller
    {
        private IJobPositionRepository _jobPositionRepository;
        private IMapper _mapper;

        public JobPositionController(IJobPositionRepository jobPositionRepository, IMapper mapper)
        {
            _jobPositionRepository = jobPositionRepository;
            _mapper = mapper;
        }
        [HttpGet("{jobPositionId}")]
        public IActionResult GetJobPosition(int jobPositionId)
        {
            if (!_jobPositionRepository.JobPositionExists(jobPositionId))
                return NotFound();

            var jobPosition = _mapper.Map<JobPositonDto>(_jobPositionRepository.GetJobPosition(jobPositionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobPosition);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetJobPositions()
        {
            var jobPositions = _mapper.Map<List<JobPositonDto>>(_jobPositionRepository.GetJobPositions());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobPositions);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJobPosition([FromBody] JobPositonDto jobPositionCreate)
        {
            if (jobPositionCreate == null)
                return BadRequest();

            var jobPosition = _jobPositionRepository.GetJobPositions().Where(c => c.Id == jobPositionCreate.Id).FirstOrDefault();

            if (jobPosition != null)
            {
                ModelState.AddModelError("", "JobPosition already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var jobPositionMap = _mapper.Map<JobPosition>(jobPositionCreate);

            if (!_jobPositionRepository.CreateJobPosition(jobPositionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
