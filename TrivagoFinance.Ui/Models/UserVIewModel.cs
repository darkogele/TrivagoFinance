using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrivagoFinance.Ui.Models
{
    public class UserVIewModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public UserRoles UserRole { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
    }

    public enum UserRoles
    {
        Employee = 1,
        TeamLead = 2,
        Finance = 3
    }
}
