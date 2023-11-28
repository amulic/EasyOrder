namespace EasyOrderAPI.Models
{
    public class JobPositions
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ICollection<JobPositionsEmployees> JobPositionsEmployees { get; set; }
    }
}
