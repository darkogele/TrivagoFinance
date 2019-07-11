using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.MokapData;
using TrivagoFinance.Ui.ViewModels;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace TrivagoFinance.Ui.Controllers.Services
{
    public interface IUserService
    {
        UserVIewModel LogIn(string email, string password);
        IEnumerable<UserVIewModel> GetAllEmployees(UserVIewModel user);
        IEnumerable<UserVIewModel> GetAllEmployeesApproved(UserVIewModel user);
        UserVIewModel Update(UserVIewModel user);
        UserVIewModel GetEmployee(int id);
        bool UploadPhoto(UserVIewModel user);
        UserVIewModel Insert(UserVIewModel user);
        bool Duplicate(string email);
        AprovalStatus ApproveStatus(UserVIewModel user);
        UserVIewModel GetEmployeeByEmail(string email);
        byte[] ExcelFIle(IEnumerable<UserVIewModel> approved);
    }

    public class UserService : IUserService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IEmployeeRepository _trivagoSqlRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IEmployeeRepository trivagoSqlRepository, IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            _trivagoSqlRepository = trivagoSqlRepository;
            _mapper = mapper;
            _emailService = emailService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<UserVIewModel> GetAllEmployees(UserVIewModel user)
        {      
            var pendingExpenses = _trivagoSqlRepository.GetPendingExpense();

            var usersFromDb = _trivagoSqlRepository.GetAllEmployees()
                .Where(e => pendingExpenses.Any(p => e.Id == p.UserId)).ToList();
          
            var usersForView = new List<UserVIewModel>();

            foreach (var employee in usersFromDb)
            {
                var employeeExpenses = pendingExpenses.Where(x => x.UserId == employee.Id).ToList();
                foreach (var ee in employeeExpenses)
                {
                    var userModel = new UserVIewModel(employee, ee);
                    usersForView.Add(userModel);
                }
            }
            return usersForView.Where(x=>x.Department == user.Department).ToList();
        }

        public UserVIewModel GetEmployee(int id)
        {
            var userFromDb = _trivagoSqlRepository.GetEmployee(id);
            if (userFromDb != null)
            {
                var userForView = _mapper.Map<UserVIewModel>(userFromDb);
                return userForView;
            }
            return null;
        }

        public UserVIewModel LogIn(string email, string password)
        {
            var passwordHash = SHA256HashGenerator.GenerateHash(password);
            var userFromDb = _trivagoSqlRepository.GetUsersByEmailAndPassword(email, passwordHash);
            if (userFromDb == null)
            {
                return null;
            }         
            var userDataInExpense = _trivagoSqlRepository.GetAllExpense().Where(e => e.UserId == userFromDb.Id).ToList();                
            if (userDataInExpense == null)
            {
                return new UserVIewModel() { Flag = "Zero" };
            }
            var everyPhotoStatus = new List<PhotoStatus>();
            foreach (var data in userDataInExpense)
            {
                var photoStatus = new PhotoStatus
                {
                    PhotoPath = data.PhotoPath,
                    AprovalStatus = data.AprovalStatus
                };
                everyPhotoStatus.Add(photoStatus);
            }
            var userForView = _mapper.Map<UserVIewModel>(userFromDb);
            userForView.PhotoStatus = everyPhotoStatus;          
            return userForView;
        }

        public UserVIewModel Update(UserVIewModel UserVIewModel)
        {
            var userForDb = _mapper.Map<User>(UserVIewModel);
            _trivagoSqlRepository.Update(userForDb);
            return UserVIewModel;
        }

        public bool UploadPhoto(UserVIewModel user)
        {
            //var foundExpense = _trivagoSqlRepository.GetExpense(user.PhotoPath);
            //if (foundExpense?.PhotoPath != null)
            //{
            //    // Delete previos Photo to save HDD/SSD space ,for keeping old photos comment this function
            //    var filePath = Path.Combine(hostingEnvironment.WebRootPath + @"\images\Users\" + foundExpense.PhotoPath);
            //    File.Delete(filePath);
            //}
            //var uploadStatus = _trivagoSqlRepository.UpdateExpense(foundExpense);
            var foundExpense = new Expense(user)
            {
                AprovalStatus = AprovalStatus.Pending,
                PhotoPath = ProcesUploadedFile(user)
            };
            var uploadStatus = _trivagoSqlRepository.InsertExpense(foundExpense);
            return uploadStatus != null ? true : false;
        }

        public UserVIewModel Insert(UserVIewModel user)
        {
            var userForDb = _mapper.Map<User>(user);
            var hashedPass = SHA256HashGenerator.GenerateHash(user.Password);
            userForDb.PasswordHash = hashedPass;
            var userFromDb = _trivagoSqlRepository.Insert(userForDb);
            var userForView = _mapper.Map<UserVIewModel>(userFromDb);
            return userForView;
        }

        public IEnumerable<UserVIewModel> GetAllEmployeesApproved(UserVIewModel user)
        {
            var userModels = new List<UserVIewModel>();
            var employees = _trivagoSqlRepository.GetAllEmployees();
            var exp = _trivagoSqlRepository.GetAllExpense();
            foreach (var employee in employees)
            {
                var employeeExpenses = exp.Where(x => x.UserId == employee.Id).ToList();
                foreach (var ee in employeeExpenses)
                {
                    var userModel = new UserVIewModel(employee, ee);
                    userModels.Add(userModel);
                }
            }

            return userModels;
        }

        public bool Duplicate(string email)
        {
            return _trivagoSqlRepository.CheekForExistingEmail(email);
        }

        public AprovalStatus ApproveStatus(UserVIewModel user)
        {
            var userFromDb = _trivagoSqlRepository.GetEmployee(user.Id);
            if (userFromDb != null)
            {
                //var status = _trivagoSqlRepository.EmployeeStatus(user.AprovalStatus, user.Id);
                var status = _trivagoSqlRepository.EmployeeStatus(user.AprovalStatus, user.PhotoPath, user.Price);
                if (status == AprovalStatus.Approved)
                {
                    var accounthing = _trivagoSqlRepository.GetAllEmployees().Where(x => x.Department == Department.Accounting); //TODO-Better logic for more then one accothing person
                    
                    // Notify Finance
                    bool accNot = true;
                    foreach (var Finance in accounthing)
                    {
                        bool result = _emailService.SendEmail(
                        Finance.Email,
                        userFromDb.FirstName,
                        "Expense receipt for " + userFromDb.FirstName.ToString(),
                        "Teach Lead sent aproved receipt");
                        accNot = accNot && result;
                    }

                    // Notify Employee
                    var mailStatusEmployee = _emailService.SendEmail(
                        userFromDb.Email,
                        userFromDb.FirstName,
                        "Expense receipt",
                        "Approved Employee Status");

                    if (mailStatusEmployee && accNot)
                    {
                        return status;
                    }
                }
            }
            return user.AprovalStatus;
        }

        public byte[] ExcelFIle(IEnumerable<UserVIewModel> approved)
        {
            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Mysheet");
            int i = 1;
            var c = sheet.CreateRow(0);
            c.CreateCell(0).SetCellValue("NAME");
            c.CreateCell(1).SetCellValue("LASTNAME");
            c.CreateCell(2).SetCellValue("EMAIL");
            c.CreateCell(3).SetCellValue("DEPARTAMENT");
            c.CreateCell(4).SetCellValue("ROLE");
            c.CreateCell(5).SetCellValue("COST");
            foreach (var item in approved)
            {
                var r = sheet.CreateRow(i);
              
                r.CreateCell(0).SetCellValue(item.FirstName);
                r.CreateCell(1).SetCellValue(item.LastName);
                r.CreateCell(2).SetCellValue(item.Email);
                r.CreateCell(3).SetCellValue(item.Department.ToString());
                r.CreateCell(4).SetCellValue(item.UserRole.ToString());
                r.CreateCell(5).SetCellValue(item.Price.ToString());

                i++;
            }

            byte[] fileContents = null;
            using (var memoryStream = new MemoryStream())
            {
                wb.Write(memoryStream);
                fileContents = memoryStream.ToArray();
            }
            return fileContents;
        }

        public UserVIewModel GetEmployeeByEmail(string email)
        {
            var user = _trivagoSqlRepository.GetEmployee(email);
            return _mapper.Map<UserVIewModel>(user);
        }

        #region Private Methods

        private string ProcesUploadedFile(UserVIewModel model)
        {
            string uniqieFileName = null;
            if (model.Photo != null && ValidationForOnlyImage(model.Photo.FileName))
            {
                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath + @"\images\Users");
                uniqieFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqieFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqieFileName;
        }

        private static bool ValidationForOnlyImage(string file)
        {
            string[] imageTypes = { "jpg", "bmp", "gif", "png" };
            bool contains = false;
            foreach (var type in imageTypes)
            {
                contains = file.Contains(type);
                if (contains)
                {
                    return contains;
                }
            }
            return contains;
        }



        #endregion
    }
}
