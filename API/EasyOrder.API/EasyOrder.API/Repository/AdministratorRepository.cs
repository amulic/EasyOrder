using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private ApplicationDbContext _context;
        public AdministratorRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }
        public ICollection<Administrator> GetAdministrators()
        {
            return _context.Administrators.ToList();
        }

        public Administrator GetAdministrator(int id)
        {
            return _context.Administrators.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool CreateAdministrator(Administrator administrator)
        {
            _context.Add(administrator);
            return Save();
        }
        public bool AdministratorExists(int adminId)
        {
            return _context.Administrators.Any(x=>x.Id == adminId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
