﻿using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.UseCases.Departments.CreateDepartment;
using ApplicationCore.UseCases.Departments.DeleteDepartment;
using ApplicationCore.UseCases.Departments.ExportExcelSheetDepartment;
using ApplicationCore.UseCases.Departments.GetDepartment;
using ApplicationCore.UseCases.Departments.GetDepartmentById;
using ApplicationCore.UseCases.Departments.UpdateDepartment;
using Azure.Core;
using EMSWebApp.Interface;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateDepartmentRequest> _validator;
        private readonly IValidator<UpdateDepartmentRequest> _updateDepartmentValidator;

        public DepartmentController(IMediator mediator, IValidator<CreateDepartmentRequest> validator, IValidator<UpdateDepartmentRequest> updateDepartmentValidator)
        {
            _mediator = mediator;
            _validator = validator;
            _updateDepartmentValidator = updateDepartmentValidator;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [Route("Department/DepartmentForm")]
        public IActionResult DepartmentForm()
        {
            return View();
        }


        [Route("Department/AddDepartmentAsync")]
        public async Task<IActionResult> DepartmentForm(CreateDepartmentRequest request)
        {
            try
            {
               var validation =  await _validator.ValidateAsync(request);

                if (!validation.IsValid)
                {
                    foreach(var i in validation.Errors)
                    {
                        ModelState.AddModelError(i.PropertyName, i.ErrorMessage);
                    }
                    return View();
                }

                var result = await _mediator.Send(request);
                return RedirectToAction("DeparmentsData");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [Route("Department/DeparmentsData")]
        public async Task<IActionResult> DeparmentsData()
        {
            try
            {
                var result = await _mediator.Send(new GetDepartmentRequest());
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Route("Department/GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetDepartmentByIdRequest {Id = id });
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Department/UpdateDepartmentName")]
        public async Task<IActionResult> GetDepartmentById(UpdateDepartmentRequest request)
        {
            try
            {
                var resultModel = await _mediator.Send(new GetDepartmentByIdRequest { Id = request.Id });

                var validation = await _updateDepartmentValidator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    foreach(var item in validation.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(resultModel);
                }

                var result = await _mediator.Send(request);
                return RedirectToAction("DeparmentsData");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Department/DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteRepository(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteDepartmentRequest { Id = id });
                return RedirectToAction("DeparmentsData");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [Route("Department/ExportEmployeeExcelSheet")]
        public async Task<IActionResult> ExportEmployeeExcelSheet(string userId, int id)
        {
            byte[] excelSheet = await _mediator.Send(new ExportExcelSheetDepartmentRequest());
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Departments.xlsx";

            return File(excelSheet, contentType, fileName);
          
        }



    }
}
