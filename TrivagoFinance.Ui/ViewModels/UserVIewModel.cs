﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrivagoFinance.Ui.ViewModels
{
    public class UserVIewModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Required]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        public string LastName { get; set; }
        [Required]
        public UserRoles UserRole { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public bool AproveStatus { get; set; }
    }

    public enum UserRoles
    {
        Employee = 1,
        TeamLead = 2,
        Finance = 3
    }
}
