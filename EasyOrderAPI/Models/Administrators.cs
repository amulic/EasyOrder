namespace EasyOrderAPI.Models
{
    public class Administrators
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PIN { get; set; }
        public Users Users { get; set; }
    }
}
