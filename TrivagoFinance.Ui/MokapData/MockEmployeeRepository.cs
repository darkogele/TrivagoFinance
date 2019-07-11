//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TrivagoFinance.Ui.Data.DomainModels;
//using TrivagoFinance.Ui.ViewModels;

//namespace TrivagoFinance.Ui.MokapData
//{
//    public class MockEmployeeRepository : IEmployeeRepository
//    {
//        private List<User> _users;
//        public MockEmployeeRepository()
//        {
//            _users = new List<User>()
//            {
//                new User(){ Id = 1, Email = "darkogele@hotmail.com", FirstName = "Darko", LastName = "Gelevski", PasswordHash = "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce", UserRole = UserRoles.Employee, PhotoPath="bd323643-d2c5-4f80-8d7e-db27716d80b2_dareto.jpg"},
//                new User(){ Id = 2, Email = "lead@trivago.com", FirstName = "Lead", LastName = "TeachLeadGuy", PasswordHash = "645978287991a6f40bcaf5840f5653b89b75b3bd1ea78cf9f39192e2400ac23e", UserRole = UserRoles.TeamLead },
//                new User(){ Id = 3, Email = "finance@trivago.com", FirstName = "Finance", LastName = "FinanceGuy", PasswordHash = "b696d75511dc16f2b52563e3113a498311a79866f4672862197aa9a8c5c0da12", UserRole = UserRoles.Finance }
//            };
//        }

//        public User Insert(User employee)
//        {
//            var user = _users.Max(x => x.Id) + 1;
//            _users.Add(employee);
//            return employee;
//        }

//        public User Delete(int Id)
//        {
//            var user = _users.FirstOrDefault(x => x.Id == Id);
//            if (user != null)
//            {
//                _users.Remove(user);
//            }
//            return user;
//        }

//        public User GetEmployee(int Id)
//        {
//            return _users.FirstOrDefault(x => x.Id == Id);
//        }

//        public IEnumerable<User> GetAllEmployees()
//        {
//            return _users.ToList();
//        }

//        public User Update(User employeeChanges)
//        {
//            var user = _users.FirstOrDefault(x => x.Id == employeeChanges.Id);
//            if (user != null)
//            {
//                user.FirstName = employeeChanges.FirstName;
//                user.LastName = employeeChanges.LastName;
//                user.UserRole = employeeChanges.UserRole;
//                user.AprovalStatus = employeeChanges.AprovalStatus;
//                return user;
//            }
//            return user;
//        }

//        public User GetUsersByEmailAndPassword(string email, string password)
//        {
//            return _users.Where(x => x.Email == email.ToLower() && x.PasswordHash == password).FirstOrDefault();
//        }

//        public bool CheekForExistingEmail(string email)
//        {
//            throw new NotImplementedException();
//        }

//        public bool EmployeeStatus(bool status, int id)
//        {
//            throw new NotImplementedException();
//        }

//        public AprovalStatus EmployeeStatus(AprovalStatus status, int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
