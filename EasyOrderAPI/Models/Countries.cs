namespace EasyOrderAPI.Models
{
    public class Countries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cities City { get; set; }
        public ICollection<Bills> Bills { get; set; }
    }
}
