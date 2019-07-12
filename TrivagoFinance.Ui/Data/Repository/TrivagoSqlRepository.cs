using System.Collections.Generic;
using System.Linq;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.MokapData;
using TrivagoFinance.Ui.ViewModels;

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

        public AprovalStatus EmployeeStatus(AprovalStatus status, string photoPath, decimal price)
        {
            var expense = _trivagoDb.Expenses.Where(x => x.PhotoPath == photoPath).FirstOrDefault();
            expense.AprovalStatus = status;
            expense.Price = price;
            _trivagoDb.Expenses.Update(expense);
            _trivagoDb.SaveChanges();
            return status;
        }

        public IEnumerable<Expense> GetAllExpense()
        {
            return _trivagoDb.Expenses.ToList();
        }

        public IEnumerable<User> GetAllEmployees()
        {
            return _trivagoDb.Users.ToList();
        }

        public User GetEmployee(int Id)
        {
            return _trivagoDb.Users.Find(Id);
        }

        public Expense GetExpense(string photoPath)
        {
            return _trivagoDb.Expenses.SingleOrDefault(x => x.PhotoPath == photoPath);
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

        // Foreign key from user 
        public Expense InsertExpense(Expense foundExpense)
        {
            if (foundExpense != null)
            {
                _trivagoDb.Expenses.Add(foundExpense);
                _trivagoDb.SaveChanges();
            }
            return foundExpense;
        }

        public IEnumerable<Expense> GetPendingExpense()
        {
            return _trivagoDb.Expenses.Where(e => e.AprovalStatus == AprovalStatus.Pending);
        }

        public User GetEmployee(string email)
        {
            return _trivagoDb.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}