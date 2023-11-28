namespace EasyOrderAPI.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Administrators? Administrators { get; set; }
        public Employees? Employees { get; set; }
    }
}
