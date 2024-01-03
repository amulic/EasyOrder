using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int Stars { get; set; }
        public Table Table { get; set; }
    }
}
