﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EasyOrder.API.Models.Domain
{
    public class User 
    { 
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }

        //public Administrator? Administrator { get; set; }
        //public Employee? Employee { get; set; }
    }
}
