using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
