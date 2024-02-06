using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public OrderController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetAdministrator(int adminId)
        {
            if (!_userRepository.UserExists(adminId))
                return NotFound();

            var user = _mapper.Map<AdministratorDto>(_userRepository.GetUser(adminId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Administrator))]
        public IActionResult GetAdministrators()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest();

            var user = _userRepository.GetUsers().Where(a => a.Id == userCreate.Id).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }
    }
}
