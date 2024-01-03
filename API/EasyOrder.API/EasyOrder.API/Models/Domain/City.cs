using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZIPCode { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public Country Country { get; set; }
    }
}
