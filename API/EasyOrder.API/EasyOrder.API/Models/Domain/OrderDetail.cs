using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOrder.API.Models.Domain
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        //dodan za modelBuilder
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderDetailItem> OrderItems { get; set; }
        //public ICollection<OrderDetailItem<Drink>> Drinks { get; set; }

    }
}
