using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public Supplier GetSupplier(Guid id)
        {
            return _context.Suppliers.Where(x => x.Id == id).FirstOrDefault();
        }
        public ICollection<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public bool SupplierExists(Guid supplierId)
        {
            return _context.Suppliers.Any(x => x.Id == supplierId);
        }

        public bool CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            return Save();
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
