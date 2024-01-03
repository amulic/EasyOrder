namespace EasyOrder.API.Models.Domain
{
    public class EmployeeJobPosition
    {
        public int EmployeeId { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public Employee Employee { get; set; }
    }
}
