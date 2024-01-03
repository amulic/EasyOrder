using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
