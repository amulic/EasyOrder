using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class JobPosition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeJobPosition> EmployeeJobPositions { get; set; }
    }
}
