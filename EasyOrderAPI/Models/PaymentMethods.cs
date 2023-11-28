namespace EasyOrderAPI.Models
{
    public class PaymentMethods
    {
        public int Id { get; set; }
        public string PaymentMetod { get; set; }
        public ICollection<Bills> Bills { get; set; }
    }
}
