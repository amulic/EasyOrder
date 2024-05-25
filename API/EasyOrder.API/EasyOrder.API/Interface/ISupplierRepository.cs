using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface ISupplierRepository
    {
        Supplier GetSupplier(Guid id);
        ICollection<Supplier> GetSuppliers();
        bool CreateSupplier(Supplier supplier);
        bool SupplierExists(Guid supplierId);
        bool Save();
    }
}
