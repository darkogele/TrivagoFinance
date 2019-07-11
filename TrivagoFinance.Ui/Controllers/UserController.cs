using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrivagoFinance.Ui.ViewModels;
using TrivagoFinance.Ui.Controllers.Services;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Net.Mime;

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
                        TempData["NewUser"] = "New User was Created";
                        return RedirectToAction("NewUserDetails", new { id = user.Id });
                    }
                }
                TempData["Error"] = "You are entering existing email address try with different one";
                return View("Employee");
            }
            var error =  ModelState.Values.SelectMany(x => x.Errors).FirstOrDefault();
            TempData["Error"] = error != null ? error.ErrorMessage : "";
            return View("Employee");
        }

        public IActionResult NewUserDetails(int id)
        {
            var user = _userService.GetEmployee(id);
            return View(user);
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
            if (user.UserRole == UserRoles.Finance)
            {
                return RedirectToAction("Accounting", user);
            }          
            return View(user);
        }

        public IActionResult TeamLead(UserVIewModel lead)
        {
            var users = _userService.GetAllEmployees(lead);
           // var my = users.Where(x => x.Department == lead.Department);
            return View(users);
        }
       
        [HttpPost]
        public IActionResult TeamLeadApproval(UserVIewModel employee)
        {
            var status = _userService.ApproveStatus(employee);
            ViewBag.Status = status;
            return View();
        }

        public IActionResult Details(int id, string photoPath)
        {
            var user = _userService.GetEmployee(id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            user.PhotoPath = photoPath;
            return View(user);
        }

        public IActionResult WrongCredential()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPhoto(UserVIewModel model)
        {
            if (model.Photo != null)
            {
                ViewBag.PhotoDone = _userService.UploadPhoto(model);
            }
            else
            {
                ViewBag.PhotoFailed = "You are not submitting Photo go back and try again";
            }
            return View(model); 
        }

        public IActionResult Accounting(UserVIewModel model)
        {
            ViewBag.Email = model.Email;
            var approved = _userService.GetAllEmployeesApproved(model);
            return View(approved);
        }

        [HttpPost]
        public IActionResult ExportToExcel(string email)
        {
            var user = _userService.GetEmployeeByEmail(email);
            var approved = _userService.GetAllEmployeesApproved(user);
            var fileContents = _userService.ExcelFIle(approved);
            //XSSFWorkbook wb = new XSSFWorkbook();
            //ISheet sheet = wb.CreateSheet("Mysheet");
            //int i = 0;
            //foreach (var item in approved)
            //{
            //    var r = sheet.CreateRow(i);

            //    r.CreateCell(0).SetCellValue(item.FirstName);
            //    r.CreateCell(1).SetCellValue(item.LastName);

            //    r.CreateCell(2).SetCellValue(item.Email);
            //    r.CreateCell(3).SetCellValue(item.Department.ToString());
            //    r.CreateCell(4).SetCellValue(item.UserRole.ToString());
            //    r.CreateCell(5).SetCellValue(item.Price.ToString());

            //    i++;
            //}

            //byte[] fileContents = null;
            //using (var memoryStream = new MemoryStream())
            //{
            //    wb.Write(memoryStream);
            //    fileContents = memoryStream.ToArray();
            //}

            return File(fileContents,MediaTypeNames.Application.Octet, "report.xlsx");
        }

    }
}