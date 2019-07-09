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
                    PhotoPath = "895d0fe3-076c-4124-8eb9-ab5424175abb_24909662_10157239995384896_4609835183655791985_n.jpg",
                    Department = Department.WebDevelopers                    
                },
                new User
                {
                    Id = 2,
                    Email = "lead@trivago.com",
                    FirstName = "Lead",
                    LastName = "TeachLeadGuy",
                    PasswordHash = "E3456BC1F4D270F4A97933758645FDC21E39642B31CA343C1818F7972AC27906",
                    UserRole = UserRoles.TeamLead,
                    Department = Department.WebDevelopers
                },
                new User
                {
                    Id = 3,
                    Email = "finance@trivago.com",
                    FirstName = "Finance",
                    LastName = "FinanceGuy",
                    PasswordHash = "b696d75511dc16f2b52563e3113a498311a79866f4672862197aa9a8c5c0da12",
                    UserRole = UserRoles.Finance,
                    Department = Department.Accounting
                },
                new User
                {
                    Id = 4,
                    Email = "employee2@trivago.com",
                    FirstName = "John",
                    LastName = "Smith",
                    PasswordHash = "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce",
                    UserRole = UserRoles.Employee,
                    PhotoPath = "b8aaa1a4-22fc-4b66-8f9f-a3da69a2de7a_travel_expense_report.png",
                    Department = Department.WebDevelopers
                },
                new User
                {
                     Id = 5,
                     Email = "employee2@trivago.com",
                     FirstName = "Rebeka",
                     LastName = "Week",
                     PasswordHash = "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce",
                     UserRole = UserRoles.Employee,
                     PhotoPath = "b8aaa1a4-22fc-4b66-8f9f-a3da69a2de7a_travel_expense_report.png",
                     Department = Department.WebDevelopers
                }
            );
        }
    }
}
