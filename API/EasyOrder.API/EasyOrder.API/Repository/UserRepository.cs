using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ActionResult<ICollection<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
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

        public async Task<User> AuthenticateUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
        public async Task CreateUserAsync(User user)
        {
            Console.WriteLine(user.Username);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return;
        }

        public Task<bool> CheckUsernameExistAsync(string username) => _context.Users.AnyAsync(x => x.Username == username);

        public Task<bool> CheckEmailExistAsync(string email) => _context.Users.AnyAsync(y => y.Email == email);
       
    }
}
