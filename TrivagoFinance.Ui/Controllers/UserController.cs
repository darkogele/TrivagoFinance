using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrivagoFinance.Ui.ViewModels;
using TrivagoFinance.Ui.Controllers.Services;

namespace TrivagoFinance.Ui.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Employee()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult CreateEmployee()
        //{           
        //    return View();
        //}

        [HttpPost]
        public IActionResult CreateEmployee(UserVIewModel userVIewModel)
        {
            if (ModelState.IsValid)
            {
                var cheekforDuplicate = _userService.Duplicate(userVIewModel.Email);
                if (cheekforDuplicate == false)
                {
                    var user = _userService.Insert(userVIewModel);
                    if (user != null)
                    {
                        TempData["NewUser"] = "New User Created";
                        return RedirectToAction("Details", user.Id);
                    }
                }
                TempData["Error"] = "You are entering existing email address try with different one";
                return View("Employee");
            }
            var error =  ModelState.Values.SelectMany(x => x.Errors).FirstOrDefault();
            TempData["Error"] = error != null ? error.ErrorMessage : "";
            return View("Employee");
        }

        [HttpPost]
        public IActionResult EmployeeLoged(string email, string password)
        {
            var user = _userService.LogIn(email, password);
            if (user == null)
            {
                return RedirectToAction("WrongCredential");
            }
            if (user.UserRole == UserRoles.TeamLead)
            {
                return RedirectToAction("TeamLead", user);
            }
           
            return View(user);
        }

        public IActionResult TeamLead(UserVIewModel lead)
        {
            var users = _userService.GetAllEmployees(lead);
            return View(users);
        }

        [HttpPost]
        public IActionResult TeamLeadApproval(UserVIewModel employee)
        {
            var status = _userService.ApproveStatus(employee);

            return View();
        }

        public IActionResult Details(int id)
        {
            var user = _userService.GetEmployee(id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
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
            ViewBag.PhotoDone = _userService.UploadPhoto(model);
            return View(model); //TO-DO Da birse stara slika
        }

    }
}