using EMSWebApp.Interface;
using EMSWebApp.Models;
using EMSWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;


namespace EMSWebApp.Controllers
{


    [Authorize]
    public class AccountController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _dptRrepository;
        private readonly UserManager<IdentityUser> _userManager;


        public AccountController(IEmployeeRepository repository, IDepartmentRepository dptRrepository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _dptRrepository = dptRrepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("Account/EmployeeRegistrationForm")]
        public async Task<IActionResult> EmployeeRegistrationForm()
        {
            var result = await _dptRrepository.GetAllDepartment();
            return View(result);
        }


        [Route("Account/AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeInformation employee, [FromForm] IFormFile image)
        {
            try
            {
                if (image == null)
                {
                    employee.ImagePath = null;
                }
                else
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string Path = "wwwroot\\" + ImageName;
                    using (var stream = new FileStream(Path, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    employee.ImagePath = ImageName;
                }


                employee.CreatedBy = _userManager.GetUserId(User);
                employee.CreatedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");


                var result = await _repository.AddEmplyee(employee);
                if (result == true)
                {
                    return RedirectToAction("GetAllEmployees");
                }
                else
                {
                    return RedirectToAction("AddEmployee");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [Route("Account/GetAllEmployees/{id?}")]
        public async Task<IActionResult> GetAllEmployees(int id)
        {
            var result = await _repository.GetAllEmployee(id);
            return View(result);
        }

        [Route("Account/UpdateEmployees/{id}")]
        public async Task<IActionResult> UpdateEmployees(int id)
        {
            var result = await _repository.GetAllEmployeeById(id);
            var res = await _dptRrepository.GetAllDepartment();

            ViewBag.Departments = res;
            return View(result);
        }

        [Route("Account/UpdateEmployeesRecord/{id}")]
        public async Task<IActionResult> UpdateEmployeesRecord(EmployeeInformation employee, [FromForm] IFormFile image, int id)
        {
            try
            {
                if (image != null)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string Path = "wwwroot\\" + ImageName;
                    using (var stream = new FileStream(Path, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    employee.ImagePath = ImageName;
                }

                else
                {
                    employee.ImagePath = null;
                }

                employee.ModifiedBy = _userManager.GetUserId(User);
                employee.ModifiedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                var result = await _repository.UpdateEmplyeesRecord(employee, id);
                if (result > 0)
                {
                    return RedirectToAction("GetAllEmployees");
                }
                return RedirectToAction("UpdateEmployees");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Route("Account/DeleteEmployees/{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {

            var result = await _repository.DeleteEmployeesRecord(id);

            return RedirectToAction("GetAllEmployees");
        }


    }
}
