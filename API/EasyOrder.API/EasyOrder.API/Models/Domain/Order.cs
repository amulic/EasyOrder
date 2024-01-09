using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOrder.API.Models.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsFinished { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Table Table { get; set; }
        [ForeignKey("OrderDetailId")]
        public int OrderDetailId { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Bill Bill { get; set; }
    }
}
