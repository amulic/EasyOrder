using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        ICollection<Order> GetOrders();
        bool CreateOrder(Order order);
        bool OrderExists(int orderId);
        bool Save();
    }
}
