using AutoMapper;
using EasyOrder.API.Helper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<OrderDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<ActionResult<User>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody]UserDto userCreate)
        //{
        //    if (userCreate == null)
        //        return BadRequest();

        //    var user = _userRepository.GetUsers().Where(a => a.Id == userCreate.Id).FirstOrDefault();

        //    if (user != null)
        //    {
        //        ModelState.AddModelError("", "User already exists.");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid) return BadRequest();

        //    var userMap = _mapper.Map<User>(userCreate);

        //    if (!_userRepository.CreateUser(userMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while saving.");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully created.");
        //}


        //JWT

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _userRepository.AuthenticateUser(userObj.Username, userObj.Password);

            if (user == null)
                return NotFound(new { Message = "User Not Found!" });

            if(!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                return BadRequest(new {Message="Password is incorrect!"});

            user.Token = CreateJwtToken(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if(userObj == null)
                return BadRequest();

            //checking username
            if(await _userRepository.CheckUsernameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username already exists!" });
            //checking email
            if (await _userRepository.CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email already exists!" });
            //checking password strenght
            var pass = CheckPasswordStrenght(userObj.Password);
            if(!string.IsNullOrEmpty(pass))
                return BadRequest(new {Message = pass});

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _userRepository.CreateUserAsync(userObj);
            
            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        private string CheckPasswordStrenght(string password)
        {
            StringBuilder sb = new StringBuilder();
            if(password.Length < 8)
                sb.Append("Minimum password lenght should be 8."+ Environment.NewLine);
            if(!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password,"[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric." + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain a special character." + Environment.NewLine);

            return sb.ToString();
            
        }

        private string CreateJwtToken(User user) 
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}")

            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
