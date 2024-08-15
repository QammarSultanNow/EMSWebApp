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
using ApplicationCore.UseCases.Employees.EmployeesExportExcel;
using FluentValidation;
using ApplicationCore.Validations;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace EMSWebApp.Controllers
{


    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateEmployeesRequest> _validator;
        private readonly IValidator<UpdateEmployeeRequest> _validatorUpdate;

        public AccountController(IMediator mediator, IValidator<CreateEmployeesRequest> validator, IValidator<UpdateEmployeeRequest> validatorUpdate)
        {
            _mediator = mediator;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
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
                ViewBag.Department = departments;
                //var result = await _dptRrepository.GetAllDepartment();
                return View();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }


        [Route("Account/AddEmployee")]
        public async Task<IActionResult> EmployeeRegistrationForm(CreateEmployeesRequest request)
        {
            try
            {
                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    foreach (var i in validation.Errors)
                    {

                        ModelState.AddModelError(i.PropertyName, i.ErrorMessage);
                    }
                    var departments = await _mediator.Send(new GetDepartmentRequest());
                    ViewBag.Department = departments;
                    return View();
                }

                request.CreatedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                //request.ImagePath = employee.ImagePath;

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

        [Route("Account/UpdateEmployeesRecord")]
        public async Task<IActionResult> UpdateEmployees(UpdateEmployeeRequest request)
        {
            try
            {
                var resultModel = await _mediator.Send(new GetEmployeesByIdRequest { Id = request.Id });
                var departments = await _mediator.Send(new GetDepartmentRequest());
                ViewBag.Departments = departments;


                var validation = await _validatorUpdate.ValidateAsync(request);

                if (!validation.IsValid)
                {
                    foreach(var item in validation.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                   
                    return View(resultModel);
                }

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
            var result = await _mediator.Send(new EmployeesExportExcelRequest(userId, id));
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Employees.xlsx";

            return File(result, contentType, fileName);
        }

       


        //public async Task<IActionResult> GetEmployeeByDepartmentId(int deprtmentId)
        //{
        //   var result =   await _repository.GetEmployeeByDepartmentId(deprtmentId);
        //    return Json(result);  
        //}
    }
}
