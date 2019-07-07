using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrivagoFinance.Data.Entities;

namespace TrivagoFinance.Data.ApplicationDbContext
{
    public class TrivagoDbContext : DbContext
    {
        public TrivagoDbContext(DbContextOptions<TrivagoDbContext> options) : base(options)
        {}

        public DbSet<User> users;
        public DbSet<Reports> Reports;
        public DbSet<TravelExpenses> TravelExpenses;
        public DbSet<UserTravelExpenses> UserTravelExpenses;
    }
}
