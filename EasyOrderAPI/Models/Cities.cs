namespace EasyOrderAPI.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZIPCode { get; set; }
        public Countries Countries { get; set; }
        public ICollection<Employees> Employees { get; set; }
        public ICollection<Suppliers> Suppliers { get; set; }

    }
}
