namespace EasyOrderAPI.Models
{
    public class Bills
    {
        public int Id { get; set; }
        public Employees Employees { get; set; }
        public PaymentMethods PaymentMethods { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
    }
}
