using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IBrandRepository
    {
        Brand GetBrand(int brandId);
        ICollection<Brand> GetBrands();
        bool CreateBrand(Brand brand);
        bool BrandExists(int brandId);
        bool Save();
    }
}
