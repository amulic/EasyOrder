namespace EasyOrderAPI.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsFinished { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
        public Tables Tables { get; set; }
        public Employees Employees { get; set; }

    }
}
