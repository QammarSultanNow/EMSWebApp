using EMSWebApp.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.DotNet.Scaffolding.Shared;
using ApplicationCore.ViewModel;
using System.Security.Claims;
using MediatR;
using ApplicationCore.UseCases.Employees.GetEmployees;
using ApplicationCore.UseCases.Employees.CreateEmployee;
using ApplicationCore.UseCases.Employees.UpdateEmployees;
using ApplicationCore.UseCases.Employees.DeleteEmployees;
using ApplicationCore.UseCases.Employees.GetEmployeesById;
using ApplicationCore.UseCases.Departments.GetDepartment;



namespace EMSWebApp.Controllers
{


    [Authorize]
    public class AccountController : Controller
    {
      
        private readonly IUploadImageService _image;
        private readonly IExportEmployeeExcelSheet _exportEmployeeExcel;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, IUploadImageService image, IExportEmployeeExcelSheet exportEmployeeExcel, ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager, IMediator mediator)
        {
            
            _userManager = userManager;
            _image = image;
            _exportEmployeeExcel = exportEmployeeExcel;
            _logger = logger;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("Account/EmployeeRegistrationForm")]
        public async Task<IActionResult> EmployeeRegistrationForm()
        {
            try
            {
                var departments = await _mediator.Send(new GetDepartmentRequest());
                //var result = await _dptRrepository.GetAllDepartment();
                return View(departments);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }


        [Route("Account/AddEmployee")]
        public async Task<IActionResult> AddEmployee(CreateEmployeesRequest request, EmployeeInformation employee, [FromForm] IFormFile image)
        {
            try
            {
                await _image.UploadImageByUser(employee, image);

                request.CreatedBy = _userManager.GetUserId(User);
                request.CreatedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                request.ImagePath = employee.ImagePath;


                 var result =  await _mediator.Send(request);
              

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
                var department = await _mediator.Send(new GetDepartmentRequest());

                //var department = await _dptRrepository.GetAllDepartment();
                ViewBag.Department = department;
                TempData["SelectedDepartmentId"] = id;
                ViewBag.SelectedDepartmentId = TempData["SelectedDepartmentId"] as int?;


                if (User.IsInRole("Admin"))
                {
                    var result = await _mediator.Send(new GetEmployeeRequest(userId, id));
                    return View(result);
                }
                else
                {
                    employee.CreatedBy = _userManager.GetUserId(User);
                    userId = employee.CreatedBy;

                    var result = await _mediator.Send(new GetEmployeeRequest(userId, id));
                    TempData["SelectedDepartmentId"] = id;
                    return View(result);
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
                var result = await _mediator.Send(new GetEmployeesByIdRequest { Id = id });

                var departments = await _mediator.Send(new GetDepartmentRequest());
                //var res = await _dptRrepository.GetAllDepartment();

                ViewBag.Departments = departments;
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Account/UpdateEmployeesRecord/{id}")]
        public async Task<IActionResult> UpdateEmployeesRecord(UpdateEmployeeRequest request, EmployeeInformation employee, [FromForm] IFormFile image, int id)
        {
            try
            {
                await _image.UploadImageByUser(employee, image);

                request.ModifiedBy = _userManager.GetUserId(User);
                request.ModifiedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                request.ImagePath = employee.ImagePath;

                var result = await _mediator.Send(request);
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
                var result = await _mediator.Send(new DeleteEmployeesRequest {Id = id });
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

        [AllowAnonymous]
        public async Task<IActionResult> signinWithGoogle(string returnUrl)
        {
            var redirectUrl = Url.Action(action: "ExternalLoginCallback", controller: "Account", values: new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            // If there is no record in AspNetUserLogins table, the user may not have a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e., insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);

                    //Then Signin the User
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }


                return View("Error");
            }
        }


        //public async Task<IActionResult> GetEmployeeByDepartmentId(int deprtmentId)
        //{
        //   var result =   await _repository.GetEmployeeByDepartmentId(deprtmentId);
        //    return Json(result);  
        //}
    }
}
