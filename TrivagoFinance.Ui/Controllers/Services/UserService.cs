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

namespace TrivagoFinance.Ui.Controllers.Services
{
    public interface IUserService
    {
        UserVIewModel LogIn(string email, string password);
        IEnumerable<UserVIewModel> GetAllEmployees(UserVIewModel user);
        UserVIewModel Update(UserVIewModel user);
        UserVIewModel GetEmployee(int id);
        bool UploadPhoto(UserVIewModel user);
        UserVIewModel Insert(UserVIewModel user);
        bool Duplicate(string email);
        AprovalStatus ApproveStatus(UserVIewModel user);
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
            // Employees with Photo only they are pending for Approve or Reject
            var usersFromDb = _trivagoSqlRepository.GetAllEmployees().Where(x => x.PhotoPath != null && x.Department == user.Department).ToList();
            var usersForView = _mapper.Map<List<UserVIewModel>>(usersFromDb);
            return usersForView.ToList();
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
            var userForView = _mapper.Map<UserVIewModel>(userFromDb);
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
            var foundUser = _trivagoSqlRepository.GetEmployee(user.Id);
            if (foundUser.PhotoPath != null)
            {
                // Delete previos Photo to save HDD/SSD space ,for keeping old photos comment this function
                var filePath = Path.Combine(hostingEnvironment.WebRootPath + @"\images\Users\" + foundUser.PhotoPath);
                File.Delete(filePath);
            }
            foundUser.AprovalStatus = AprovalStatus.Pending;
            foundUser.PhotoPath = ProcesUploadedFile(user);
            var uploadStatus = _trivagoSqlRepository.Update(foundUser);
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

        public bool Duplicate(string email)
        {
            return _trivagoSqlRepository.CheekForExistingEmail(email);
        }

        public AprovalStatus ApproveStatus(UserVIewModel user) // TODO changes in DB stored must be 
        {
            var userFromDb = _trivagoSqlRepository.GetEmployee(user.Id);
            if (userFromDb != null)
            {
                var status = _trivagoSqlRepository.EmployeeStatus(user.AprovalStatus, user.Id);
                if (status == AprovalStatus.Approved)
                {
                    var accounthing = _trivagoSqlRepository.GetAllEmployees().Where(x => x.Department == Department.Accounting).FirstOrDefault(); // Better logic for more then one accothing person
                    // Notify Finance
                    var accNot = _emailService.SendEmail(
                        accounthing.Email,
                        userFromDb.FirstName,
                        "Expense receipt for " + userFromDb.FirstName.ToString(),
                        "Teach Lead sent aproved receipt");
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
