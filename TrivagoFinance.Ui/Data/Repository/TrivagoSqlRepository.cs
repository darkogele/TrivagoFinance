using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.MokapData;

namespace TrivagoFinance.Ui.Data.Repository
{
    public class TrivagoSqlRepository : IEmployeeRepository
    {
        private TrivagoDbContext _trivagoDb;
        public TrivagoSqlRepository(TrivagoDbContext trivagoDb)
        {
            _trivagoDb = trivagoDb;
        }

        public bool CheekForExistingEmail(string email)
        {
            var cheek = _trivagoDb.Users.Where(x => x.Email == email).FirstOrDefault();
            return cheek != null ? true : false;
        }

        public User Delete(int Id)
        {
            var user = _trivagoDb.Users.Find(Id);
            if (user != null)
            {
                _trivagoDb.Users.Remove(user);
                _trivagoDb.SaveChanges();
            }
            return user;
        }

        public bool EmployeeStatus(bool status, int id)
        {
            var user = _trivagoDb.Users.Find(id);
            user.AproveStatus = status;
            _trivagoDb.Users.Update(user);
            _trivagoDb.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAllEmployees()
        {
            return _trivagoDb.Users.ToList();
        }

        public User GetEmployee(int Id)
        {
            return _trivagoDb.Users.Find(Id);
        }

        // SEND SHA256 before
        public User GetUsersByEmailAndPassword(string email, string password) 
        {
            return _trivagoDb.Users.FirstOrDefault(x => x.Email == email && x.PasswordHash == password); 
        }

        public User Insert(User employee)
        {
            _trivagoDb.Users.Add(employee);
            _trivagoDb.SaveChanges();
            return employee;
        }

        // What will be updated in service layer here we send Full modifed object
        public User Update(User employeeChanges)
        {
            var user = _trivagoDb.Users.Find(employeeChanges.Id);
            if (user != null)
            {
                _trivagoDb.Users.Update(employeeChanges); 
                _trivagoDb.SaveChanges();
            }
            return user;
        }
    }
}
