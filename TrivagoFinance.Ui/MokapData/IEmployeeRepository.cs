using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.DomainModels;

namespace TrivagoFinance.Ui.MokapData
{
    public interface IEmployeeRepository
    {
        User GetEmployee(int Id);
        IEnumerable<User> GetAllEmployees();
        User Insert(User employee);
        User Update(User employeeChanges);
        User Delete(int Id);
        User GetUsersByEmailAndPassword(string email, string password);
        bool CheekForExistingEmail(string email);
        bool EmployeeStatus(bool status, int id);
    }
}
