namespace EasyOrderAPI.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Products Products { get; set; }
        public Orders Orders { get; set; }
        public Bills Bills { get; set; }

    }
}
