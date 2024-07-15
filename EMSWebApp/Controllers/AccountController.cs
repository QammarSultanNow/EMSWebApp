using EMSWebApp.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;


namespace EMSWebApp.Controllers
{


    [Authorize]
    public class AccountController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _dptRrepository;
        private readonly IUploadImageService _image;
        private readonly IExportEmployeeExcelSheet _exportEmployeeExcel;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<AccountController> _logger;

        public AccountController(IEmployeeRepository repository, IDepartmentRepository dptRrepository, UserManager<IdentityUser> userManager, IUploadImageService image, IExportEmployeeExcelSheet exportEmployeeExcel, ILogger<AccountController> logger)
        {
            _repository = repository;
            _dptRrepository = dptRrepository;
            _userManager = userManager;
            _image = image;
            _exportEmployeeExcel = exportEmployeeExcel;
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}


        [Route("Account/EmployeeRegistrationForm")]
        public async Task<IActionResult> EmployeeRegistrationForm()
        {
            try
            {
                var result = await _dptRrepository.GetAllDepartment();
                return View(result);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }


        [Route("Account/AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeInformation employee, [FromForm] IFormFile image)
        {
            try
            {
                await _image.UploadImageByUser(employee, image);

                employee.CreatedBy = _userManager.GetUserId(User);
                employee.CreatedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                var result = await _repository.AddEmplyee(employee);

                _logger.LogInformation("Added employee {EmployeeName} with ID {EmployeeId}", employee.Name, employee.Id);
                return RedirectToAction("GetAllEmployees");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Route("Account/GetAllEmployees/{id?}")]
        public async Task<IActionResult> GetAllEmployees(EmployeeInformation employee, string userId,   int id )
        {
            try
            {
               
                if (User.IsInRole("Admin"))
                {
                    var result = await _repository.GetAllEmployee(userId, id);
                    return View(result);
                }
                else
                {
                    employee.CreatedBy = _userManager.GetUserId(User);
                    userId = employee.CreatedBy;

                    var userResults = await _repository.GetAllEmployee(userId, id);
                    return View(userResults);
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Account/UpdateEmployees/{id}")]
        public async Task<IActionResult> UpdateEmployees(int id)
        {
            try
            {
                var result = await _repository.GetAllEmployeeById(id);
                var res = await _dptRrepository.GetAllDepartment();

                ViewBag.Departments = res;
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Account/UpdateEmployeesRecord/{id}")]
        public async Task<IActionResult> UpdateEmployeesRecord(EmployeeInformation employee, [FromForm] IFormFile image, int id)
        {
            try
            {
                await _image.UploadImageByUser(employee, image);

                employee.ModifiedBy = _userManager.GetUserId(User);
                employee.ModifiedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                var result = await _repository.UpdateEmplyeesRecord(employee, id);
                return RedirectToAction("GetAllEmployees");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Route("Account/DeleteEmployees/{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            try
            {
                var result = await _repository.DeleteEmployeesRecord(id);
                return RedirectToAction("GetAllEmployees");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Route("Account/ExportEmployeeExcelSheet")]
        public async Task<IActionResult> ExportEmployeeExcelSheet(string userId, int id)
        {
          byte[] excelSheet = await  _exportEmployeeExcel.DownloadEmployeeExcelSheet(userId, id);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Employees.xlsx";

            return File(excelSheet, contentType, fileName);
        }
    }
}
