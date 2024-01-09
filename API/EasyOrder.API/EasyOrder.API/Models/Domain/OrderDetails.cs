using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOrder.API.Models.Domain
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
