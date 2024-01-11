using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyOrder.API.Repository
{
    public class CityRepository : ICityRepository
    {
        private ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(x => x.Id == cityId);
        }

        public bool CreateCity(City city)
        {
            _context.Cities.Add(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public City GetCity(int cityId)
        {
            return _context.Cities.Where(x => x.Id == cityId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
