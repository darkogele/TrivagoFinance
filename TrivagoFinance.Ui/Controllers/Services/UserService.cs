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
        IEnumerable<User> GetAllEmployees();
        UserVIewModel Update(UserVIewModel user);
        UserVIewModel GetEmployee(int id);
        bool UploadPhoto(UserVIewModel user);
        UserVIewModel Insert(UserVIewModel user);
    }

    public class UserService : IUserService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IEmployeeRepository _trivagoSqlRepository;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IEmployeeRepository trivagoSqlRepository, IHostingEnvironment hostingEnvironment)
        {
            _trivagoSqlRepository = trivagoSqlRepository;
            _mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<User> GetAllEmployees()
        {
            return _trivagoSqlRepository.GetAllEmployees();
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
            foundUser.PhotoPath = ProcesUploadedFile(user);
            var uploadStatus = _trivagoSqlRepository.Update(foundUser);
            return uploadStatus != null ? true: false;          
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

        public UserVIewModel Insert(UserVIewModel user) // TODO Create on User
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
