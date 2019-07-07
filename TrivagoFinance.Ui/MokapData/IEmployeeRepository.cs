using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Models;

namespace TrivagoFinance.Ui.MokapData
{
    public interface IEmployeeRepository
    {
        UserVIewModel GetEmployee(int Id);       
        IEnumerable<UserVIewModel> GetAllEmployees();
        UserVIewModel Insert(UserVIewModel employee);
        UserVIewModel Update(UserVIewModel employeeChanges);
        UserVIewModel Delete(int Id);
        UserVIewModel GetUsersByEmailAndPassword(string email, string password);
    }
}
