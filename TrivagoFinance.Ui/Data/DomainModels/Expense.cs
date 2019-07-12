using System.ComponentModel.DataAnnotations;
using TrivagoFinance.Ui.ViewModels;

namespace TrivagoFinance.Ui.Data.DomainModels
{
    public class Expense
    {
        public Expense()
        {
        }

        public Expense(UserVIewModel user)
        {
            UserId = user.Id;
        }

        [Key]
        public int Id { get; set; }

        public string PhotoPath { get; set; }
        public AprovalStatus AprovalStatus { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}