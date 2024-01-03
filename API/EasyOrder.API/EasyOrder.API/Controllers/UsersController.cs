using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //[HttpPost]
        //public async Task<IActionResult> CreateUser(CreateUserRequestDto req)
        //{
        //    var user = new User
        //    {
        //        Username = req.Username,
        //        Password = req.Password,
        //        Email = req.Email,
        //        FirstName = req.FirstName,
        //        LastName = req.LastName
        //    };
        //}
    }
}
