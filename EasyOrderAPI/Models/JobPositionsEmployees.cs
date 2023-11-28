namespace EasyOrderAPI.Models
{
    public class JobPositionsEmployees
    {
        public int EmployeeId { get; set; }
        public int JobPositionId { get; set; }
        public Employees Employee { get; set; }
        public JobPositions JobPosition { get; set; }

    }
}
