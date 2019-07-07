using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrivagoFinance.Ui.Models;
using TrivagoFinance.Ui.MokapData;
using Microsoft.AspNetCore.Hosting;

namespace TrivagoFinance.Ui.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmployeeRepository _mockEmployeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public UserController(IEmployeeRepository mockEmployeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _mockEmployeeRepository = mockEmployeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Employee()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult EmployeeLoged(string email, string password)
        {
            var user = _mockEmployeeRepository.GetUsersByEmailAndPassword(email, password);
            if (user == null)
            {
               return RedirectToAction("WrongCredential");
            }
            return View(user);
        }

        public IActionResult WrongCredential()
        {          
            return View();
        }

        [HttpPost]
        public IActionResult UploadPhoto(UserVIewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PhotoPath = ProcesUploadedFile(model);
                _mockEmployeeRepository.Insert(model);
                ViewBag.PhotoDone = true;
            }          
            return View(model);
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