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

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployee(UserVIewModel userVIewModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmployeeLoged(string email, string password)
        {
            var user = _userService.LogIn(email, password);
            if (user.UserRole == UserRoles.TeamLead)
            {
                return RedirectToAction("TeamLead");
            }
            if (user == null)
            {
                return RedirectToAction("WrongCredential");
            }
            return View(user);
        }

        public IActionResult TeamLead()
        {
            var users = _userService.GetAllEmployees();
            return View(users);
        }

        [HttpPost]
        public IActionResult TeamLead(bool approvedStatus)
        {
            
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
            if (ModelState.IsValid)
            {
                _userService.UploadPhoto(model);
               
            }
            return View(model); // Sjebano View
        }
     
    }
}