using System;
using System.Collections.Generic;
using System.Text;

namespace TrivagoFinance.Data.Entities
{
    public class UserTravelExpenses
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public TravelExpenses TravelExpensesId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
