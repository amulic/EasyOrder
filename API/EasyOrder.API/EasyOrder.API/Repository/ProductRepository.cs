using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(x=>x.Id == productId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;    
        }
    }
}
