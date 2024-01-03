using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
