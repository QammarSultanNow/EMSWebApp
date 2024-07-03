using EMSWebApp.Interface;
using EMSWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("Department/DepartmentForm")]
        public IActionResult DepartmentForm()
        {
            return View();
        }


        [Route("Department/AddDepartmentAsync")]
        public async Task<IActionResult> AddDepartmentAsync(Department department)
        {
            try
            {
                var result = await _repository.AddDepartment(department);
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
                var result = await _repository.EmployeeCount();
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
                var result = await _repository.GetAllDepartmentById(id);
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Department/UpdateDepartmentName/{id}")]
        public async Task<IActionResult> UpdateDepartmentName(Department department, int id)
        {
            try
            {
                var result = await _repository.UpdateDepartment(department, id);
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
                var result = await _repository.DeleteDepartment(id);
                return RedirectToAction("DeparmentsData");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



    }
}
