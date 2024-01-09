using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOrder.API.Models.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int InStock { get; set; }
        public Brand Brand { get; set; }
        public Supplier Supplier { get; set; }
        [ForeignKey("OrderDetailId")]
        public int OrderDetailId { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
