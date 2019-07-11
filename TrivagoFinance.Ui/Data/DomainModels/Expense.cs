using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.DomainModels;
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

        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        [DisplayName("Aproval Status")]
        public AprovalStatus AprovalStatus { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
