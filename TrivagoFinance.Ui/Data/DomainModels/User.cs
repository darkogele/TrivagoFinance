using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.ViewModels;

namespace TrivagoFinance.Ui.Data.DomainModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string LastName { get; set; }
        public UserRoles UserRole { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
        
        public AprovalStatus AprovalStatus { get; set; }
    }
}
