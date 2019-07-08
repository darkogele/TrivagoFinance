using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.ViewModels;

namespace TrivagoFinance.Ui.Data.SeedData
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 1,
                    Email = "darkogele@hotmail.com",
                    FirstName = "Darko",
                    LastName = "Gelevski",
                    PasswordHash = "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce",
                    UserRole = UserRoles.Employee,
                    PhotoPath = "bd323643-d2c5-4f80-8d7e-db27716d80b2_dareto.jpg"
                },
                new User
                {
                    Id = 2,
                    Email = "lead@trivago.com",
                    FirstName = "Lead",
                    LastName = "TeachLeadGuy",
                    PasswordHash = "645978287991a6f40bcaf5840f5653b89b75b3bd1ea78cf9f39192e2400ac23e",
                    UserRole = UserRoles.TeamLead
                },
                new User
                {
                    Id = 3,
                    Email = "finance@trivago.com",
                    FirstName = "Finance",
                    LastName = "FinanceGuy",
                    PasswordHash = "b696d75511dc16f2b52563e3113a498311a79866f4672862197aa9a8c5c0da12",
                    UserRole = UserRoles.Finance
                }
            );
        }
    }
}
