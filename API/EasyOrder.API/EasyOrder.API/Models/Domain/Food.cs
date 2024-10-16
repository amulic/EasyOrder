using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace EasyOrder.API.Models.Domain
{
    public class Food : Item
    {
        [Key]
        public int FoodId { get; set; }
            
        public string? ImageLink { get; set; }
        
        //public virtual ICollection<Product> Products { get; set; }
        
        //public virtual ICollection<OrderDetailItem<Food>> OrderDetailsItems { get; set; }

    }
}
