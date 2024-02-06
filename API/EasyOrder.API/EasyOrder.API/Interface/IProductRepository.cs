using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        ICollection<Product> GetProducts();
        bool CreateProduct(Product product);
        bool ProductExists(int productId);
        bool Save();
    }
}
