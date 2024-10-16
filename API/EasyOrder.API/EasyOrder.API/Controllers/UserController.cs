using AutoMapper;
using EasyOrder.API.Helper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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

        
        //[HttpGet("me2")]
        //[ProducesResponseType(200, Type = typeof(User))]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult<User>> GetMe()
        //{
        //    var authToken = Request.Cookies["authToken"];
        //    Console.WriteLine(authToken);
        //    if (string.IsNullOrEmpty(authToken))
        //    {
        //        return Unauthorized("Token is missing");
        //    }

        //    // Validate the token to extract the userId
        //    int userId = TokenValidator.ValidateToken(authToken);
        //    if (userId == 0) // ValidateToken returns 0 if token is invalid
        //    {
        //        return Unauthorized("Invalid token");
        //    }

        //    // Retrieve the user details using the userId
        //    var user = await _userRepository.GetUser(userId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found");
        //    }

        //    // Return the user details as a response
        //    return Ok(user);
        //}

        
        [HttpGet("me")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> GetUserMe()
        {
            User user = null;
            Request.Cookies.TryGetValue("authToken", out string? token);
           
            if (token == null)
                return BadRequest();
            else
            {
                var userId = TokenValidator.ValidateToken(token);
                Console.WriteLine(userId);
                if (userId != null)
                {
                    user = await _userRepository.GetUser(userId);
                }
                else
                    return BadRequest(new { Message = "Something went wrong while validating or returning from database!" });
            }

            try
            {
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<ActionResult<User>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }


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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Should be true in production for HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(1),
                // Ensure the path is set if required
            };

            Response.Cookies.Append("authToken", user.Token, cookieOptions);

            return Ok(new
            {
                //Token = user.Token,
                Message = "Login Success!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            //checking username
            if (await _userRepository.CheckUsernameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username already exists!" });
            //checking email
            if (await _userRepository.CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email already exists!" });
            //checking password strenght
            var pass = CheckPasswordStrenght(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            //userObj.Role = "User";
            userObj.Token = "";
            await _userRepository.CreateUserAsync(userObj);

            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(-2)
            };
            
            Response.Cookies.Append("authToken", "", cookieOptions);
            return Ok(new { Message = "Logged out successfully" });
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
