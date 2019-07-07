using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Models;

namespace TrivagoFinance.Ui.MokapData
{  
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<UserVIewModel> _users;
        public MockEmployeeRepository()
        {
            _users = new List<UserVIewModel>()
            {
                new UserVIewModel(){ Id = 1, Email = "darkogele@hotmail.com", FirstName = "Darko", LastName = "Gelevski", Password = "usako756", UserRole = UserRoles.Employee },
                new UserVIewModel(){ Id = 2, Email = "lead@trivago.com", FirstName = "Lead", LastName = "TeachLeadGuy", Password = "Lead", UserRole = UserRoles.TeamLead },
                new UserVIewModel(){ Id = 3, Email = "finance@trivago.com", FirstName = "Finance", LastName = "FinanceGuy", Password = "Finance", UserRole = UserRoles.Finance }
            };
        }

        public UserVIewModel Insert(UserVIewModel employee)
        {
            var user = _users.Max(x => x.Id) + 1;
            _users.Add(employee);
            return employee;
        }

        public UserVIewModel Delete(int Id)
        {
            var user = _users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return user;
        }

        public UserVIewModel GetEmployee(int Id)
        {
            return _users.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<UserVIewModel> GetAllEmployees()
        {
            return _users.ToList();
        }

        public UserVIewModel Update(UserVIewModel employeeChanges)
        {
            var user = _users.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (user != null)
            {
                user.FirstName = employeeChanges.FirstName;
                user.LastName = employeeChanges.LastName;
                user.UserRole = employeeChanges.UserRole;
                return user;
            }
            return user;
        }

        public UserVIewModel GetUsersByEmailAndPassword(string email, string password)
        {
            return _users.Where(x => x.Email == email.ToLower() && x.Password == password).FirstOrDefault();         
        }
    }
}
