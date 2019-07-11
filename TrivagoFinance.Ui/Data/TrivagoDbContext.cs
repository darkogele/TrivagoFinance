using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.SeedData;
using TrivagoFinance.Ui.Data.DomainModels;

namespace TrivagoFinance.Ui.Data
{
    public class TrivagoDbContext : DbContext
    {
        public TrivagoDbContext(DbContextOptions<TrivagoDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
