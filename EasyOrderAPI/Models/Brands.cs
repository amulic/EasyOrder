namespace EasyOrderAPI.Models
{
    public class Brands
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
