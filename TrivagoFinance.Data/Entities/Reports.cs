using System;
using System.Collections.Generic;
using System.Text;

namespace TrivagoFinance.Data.Entities
{
    public class Reports
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
