using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public   string FirstName { get; set; }
        public string LastName { get; set; }

        public Administrator? Administrator { get; set; }
        public Employee? Employee { get; set; }
    }
}
