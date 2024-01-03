using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string QRCodeURL { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Rating> Rating { get; set; }
    }
}
