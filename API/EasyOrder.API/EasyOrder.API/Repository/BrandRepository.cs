using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool BrandExists(int brandId)
        {
            return _context.Brands.Any(x=>x.Id == brandId);
        }

        public bool CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            return Save();
        }

        public Brand GetBrand(int brandId)
        {
            return _context.Brands.Where(x => x.Id == brandId).FirstOrDefault();
        }

        public ICollection<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
