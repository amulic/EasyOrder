using EasyOrder.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<ActionResult<ICollection<User>>> GetUsers();
        bool CreateUser(User user);
        bool UserExists(int userId);
        Task<User> AuthenticateUser(string username, string password);
        bool Save();
        Task CreateUserAsync(User user);
        Task<bool> CheckUsernameExistAsync(string username);
        Task<bool> CheckEmailExistAsync(string email);

    }
}
