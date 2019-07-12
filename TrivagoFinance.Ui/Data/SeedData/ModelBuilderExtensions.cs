using Microsoft.EntityFrameworkCore;
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
                    Email = "employee@trivago.com",
                    FirstName = "John",
                    LastName = "Do",
                    PasswordHash = "2FDC0177057D3A5C6C2C0821E01F4FA8D90F9A3BB7AFD82B0DB526AF98D68DE8",
                    UserRole = UserRoles.Employee,
                    //PhotoPath = "895d0fe3-076c-4124-8eb9-ab5424175abb_24909662_10157239995384896_4609835183655791985_n.jpg",
                    Department = Department.WebDeveloper
                },
                new User
                {
                    Id = 2,
                    Email = "lead@trivago.com",
                    FirstName = "Lead",
                    LastName = "TeachLeadGuy",
                    PasswordHash = "E3456BC1F4D270F4A97933758645FDC21E39642B31CA343C1818F7972AC27906",
                    UserRole = UserRoles.TeamLead,
                    Department = Department.WebDeveloper
                },
                new User
                {
                    Id = 3,
                    Email = "finance@trivago.com",
                    FirstName = "Finance",
                    LastName = "FinanceGuy",
                    PasswordHash = "EAB762A03FD979A04CC4706E6536D382BC89D2D1356AFCD054A16B2235ECD471",
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
                    //PhotoPath = "b8aaa1a4-22fc-4b66-8f9f-a3da69a2de7a_travel_expense_report.png",
                    Department = Department.WebDeveloper
                },
                new User
                {
                    Id = 5,
                    Email = "employee3@trivago.com",
                    FirstName = "Rebeka",
                    LastName = "Week",
                    PasswordHash = "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce",
                    UserRole = UserRoles.Employee,
                    //PhotoPath = "b8aaa1a4-22fc-4b66-8f9f-a3da69a2de7a_travel_expense_report.png",
                    Department = Department.WebDeveloper
                }
            );
        }
    }
}