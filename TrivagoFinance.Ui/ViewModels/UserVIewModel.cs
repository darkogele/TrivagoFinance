using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrivagoFinance.Ui.Data.DomainModels;

namespace TrivagoFinance.Ui.ViewModels
{
    public class UserVIewModel
    {
        public UserVIewModel()
        {

        }

        public UserVIewModel(User employee, Expense ee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Department = employee.Department;
            Email = employee.Email;
            UserRole = employee.UserRole;
            //Photo = ee.Photo;
            PhotoPath = ee.PhotoPath;
            AprovalStatus = ee.AprovalStatus;
            Price = ee.Price;
        }

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
        [Required]
        public Department Department { get; set; }

        public decimal Price { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
    //public List<string> AllPhotos { get; set; }
        [DisplayName("Aproval Status")]
        public AprovalStatus AprovalStatus { get; set; }
    //public List<AprovalStatus> EveryAprovalStatus { get; set; }
        public List<PhotoStatus> PhotoStatus { get; set; }
        public string Flag { get; set; }
    }
    public class PhotoStatus
    {
        public AprovalStatus AprovalStatus { get; set; }
        public string PhotoPath { get; set; }
    }

    public enum UserRoles
    {
        Employee = 1,
        TeamLead = 2,
        Finance = 3
    }

    public enum Department
    {
        Services = 1,
        Training = 2,
        Marketing = 3,
        Legal = 4,
        [Display(Name ="Human Resources")]
        HumanResources = 5,
        IT = 6,
        [Display(Name = "Finance")]
        Accounting = 7,
        Support = 8,
        [Display(Name = "Web Developer")]
        WebDeveloper = 9
    }

    public enum AprovalStatus
    {
        Pending,
        Declined,
        Approved
    }
}
