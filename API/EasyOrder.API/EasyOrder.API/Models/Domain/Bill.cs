using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime BillingDate { get; set; }
        public ICollection<Order> Order { get; set; }
        public Employee Employee { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
