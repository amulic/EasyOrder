using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public City City { get; set; }
    }
}
