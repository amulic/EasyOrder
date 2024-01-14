using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetAdministrator(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(employeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public IActionResult GetAdministrators()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
                return BadRequest();

            var employee = _employeeRepository.GetEmployees().Where(a => a.Id == employeeCreate.Id).FirstOrDefault();

            if (employee != null)
            {
                ModelState.AddModelError("", "Employee already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var employeeMap = _mapper.Map<Employee>(employeeCreate);

            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }
    }
}
