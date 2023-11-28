namespace EasyOrderAPI.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int InStock { get; set; }

        public ICollection<OrderDetails> Details { get; set; }
        public Brands Brand { get; set; }
    }
}
