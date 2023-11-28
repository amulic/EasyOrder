namespace EasyOrderAPI.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HoursOfWorkExpereince { get; set; } 
        public DateTime EmploymentDate { get; set; }
        public string Address { get; set; }
        public Users User { get; set; }
        public ICollection<JobPositionsEmployees> JobPositionsEmployees { get; set; }
        public Cities City { get; set; }
        public ICollection<Bills> Bills { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
