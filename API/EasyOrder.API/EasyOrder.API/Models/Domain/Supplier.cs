using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOrder.API.Models.Domain
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        //dodan za modelBuilder
        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
