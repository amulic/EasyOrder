using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
