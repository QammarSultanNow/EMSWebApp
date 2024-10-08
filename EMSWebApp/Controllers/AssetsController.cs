﻿using ApplicationCore.Interfaces;
using ApplicationCore.AssetsModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Enum;
using Infrastructure.Services;
using EMSWebApp.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.UseCases.Assets.CreateAssets;
using MediatR;
using ApplicationCore.UseCases.Assets.GetAssets;
using ApplicationCore.UseCases.Assets.UpdateAssets;
using ApplicationCore.UseCases.Assets.DeleteAssets;
using ApplicationCore.UseCases.Assets.AssignAsset;
using ApplicationCore.UseCases.Assets.AddAssignedAsset;
using ApplicationCore.UseCases.Assets.ListAssignedAsset;
using ApplicationCore.UseCases.Assets.UnassignAssets;
using ApplicationCore.UseCases.Assets.GetEmployeeOnDptId;
using ApplicationCore.UseCases.Departments.GetDepartment;
using ApplicationCore.UseCases.Assets.GetAssestById;
using ApplicationCore.UseCases.Assets.ExportExcelSheet;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EMSWebApp.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateAssetsRequst> _validator;
        private readonly IValidator<UpdateAssetsRequest> _updateValidator;
        public AssetsController(IExportEmployeeExcelSheet exportAseetExcelSheet, IUploadImageService uploadImageService, IDepartmentRepository departmentRepository, IMediator mediator, IValidator<CreateAssetsRequst> validator, IValidator<UpdateAssetsRequest> updateValidator)
        {
            _mediator = mediator;
            _validator = validator;
            _updateValidator = updateValidator;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAssets()
        {
            return View();
        }

        [HttpPost]
        [Route("Assets/AddAssets")]
        public async Task<IActionResult> AddAssets(CreateAssetsRequst requst)
        {
            var validation = await  _validator.ValidateAsync(requst);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();

            }


            requst.CreatedAt = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            requst.Status = EssetEnum.Available.ToString();

            var res =  await _mediator.Send(requst);
            return RedirectToAction("GetAssetRecords");
            
        }

        public async Task<IActionResult> GetAssetRecords()
        {
            try
            {
                var result = await _mediator.Send(new GetAssetsRequest());
                if (result == null)
                {
                    throw new Exception();
                }
               
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> GetAssetsRecordById(int id)
        {
            var result = await _mediator.Send(new GetAssestByIdRequest() { Id= id}) ;
            return View(result);
        }

        [HttpPost]
        [Route("Assets/UpdateAssetsRecords")]
        public async Task<IActionResult> GetAssetsRecordById(UpdateAssetsRequest requst)
        {
            var resultModel = await _mediator.Send(new GetAssestByIdRequest() { Id = requst.Id });

            var validation = await _updateValidator.ValidateAsync(requst);
            if (!validation.IsValid)
            {
                foreach(var item in validation.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(resultModel);
            }
            requst.ModifiedAt = DateTime.Now;
           // requst.ImagePath = asset.ImagePath;


           var result = await _mediator.Send(requst);
            return RedirectToAction("GetAssetRecords");
        }


        [Route("Assets/DeleteAssetsRecords/{id}")]
        public async Task<IActionResult> DeleteAssetsRecords(int id)
        {
            var result = await _mediator.Send(new DeleteAssetsRequest() { Id = id});

            if (result > 0)
            {
                return RedirectToAction("GetAssetRecords");
            }
            return View("AddAssets");
        }


        public async Task<IActionResult> AssignAssets(int id)
        {
            var result = await _mediator.Send(new AssignAssetRequest() { Id = id}) ;
            var dptData =  await _mediator.Send(new GetDepartmentRequest());
            ViewBag.Department = dptData;

            return View(result);
        }

        [HttpPost]
        [Route("Assets/AssignAssets")]
        public async Task<IActionResult> AssignAssets(AddAssignedAssetRequest request )
        {
            var result = await _mediator.Send(request);
            if (result > 0)
            {
                return RedirectToAction("GetAssetRecords");
            }
            return View();
        }

        public async Task<IActionResult> ListAssestWithEmployee(int id)
        {
            var result = await _mediator.Send(new ListAssignedAssetRequest() { Id = id}) ;
            return View(result);
        }

        public async Task<IActionResult> UnassignAsset(int id)
        {
            var result = await _mediator.Send(new UnassignAssetRequest() { Id = id}) ;
            return RedirectToAction("GetAssetRecords");
        }


        [HttpGet]
        [Route("Assets/GetEmployeeListOnDepartmentId")]
        public async Task<IActionResult> GetEmployeeListOnDepartmentId(int departmentId)
        {
          var result =  await _mediator.Send(new GetEmployeeOnDptIdRequest() { Id = departmentId }) ;
          var employeeList = new SelectList(result, "Id" , "Name");
          return View(employeeList);
        }


        [Route("Assets/AssetExcelSheet")]
        public async Task<IActionResult> AssetExcelSheet()
        {
           var result = await _mediator.Send(new ExportExcelSheetRequest());
           var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
           var fileName = "Employees.xlsx";

            return File(result, contentType, fileName);
        }
    }
}
