using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.UseCases.Departments.CreateDepartment;
using ApplicationCore.UseCases.Departments.DeleteDepartment;
using ApplicationCore.UseCases.Departments.GetDepartment;
using ApplicationCore.UseCases.Departments.GetDepartmentById;
using ApplicationCore.UseCases.Departments.UpdateDepartment;
using EMSWebApp.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IExportEmployeeExcelSheet _exportEmployeeExcel;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentRepository repository, IExportEmployeeExcelSheet exportEmployeeExcel, ILogger<DepartmentController> logger, IMediator mediator)
        {
            _exportEmployeeExcel = exportEmployeeExcel;
            _logger = logger;

            _mediator = mediator;
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
        public async Task<IActionResult> AddDepartmentAsync(CreateDepartmentRequest request)
        {
            try
            {
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

        [Route("Department/UpdateDepartmentName/{id}")]
        public async Task<IActionResult> UpdateDepartmentName(UpdateDepartmentRequest request, Department department, int id)
        {
            try
            {
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
            byte[] excelSheet = await _exportEmployeeExcel.DownloadDepartmentExcelSheet();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Departments.xlsx";

            return File(excelSheet, contentType, fileName);
          
        }



    }
}
