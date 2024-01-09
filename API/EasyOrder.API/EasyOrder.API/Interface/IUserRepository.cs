using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IUserRepository
    {
        User GetUser(int id);
        ICollection<User> GetUsers();
        bool CreateUser(User user);
        bool UserExists(int userId);
        bool Save();
    }
}
