using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; }
        public int PIN { get; set; }
        public User User { get; set; }
    }
}
