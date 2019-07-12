using Microsoft.EntityFrameworkCore;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.Data.SeedData;

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