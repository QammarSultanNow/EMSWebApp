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
            var result = await _repository.AddDepartment(department);
            if (result == true)
            {
                return RedirectToAction("DeparmentsData");
            }

            return RedirectToAction("AddDepartmentAsync");
        }

       
        [Route("Department/DeparmentsData")]
        public async Task<IActionResult> DeparmentsData()
        {
            var result = await _repository.GetAllDepartment();
            return View(result);
        }

        [Route("Department/GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _repository.GetAllDepartmentById(id);
            return View(result);
        }

        [Route("Department/UpdateDepartmentName/{id}")]
        public async Task<IActionResult> UpdateDepartmentName(Department department, int id)
        {
            var result = await _repository.UpdateDepartment(department, id);
            if(result > 0)
            {
                return RedirectToAction("DeparmentsData");
            }
            return RedirectToAction("AddDepartmentAsync");
        }

        [Route("Department/DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteRepository(int id)
        {
            var result = await _repository.DeleteDepartment(id);
            return RedirectToAction("DeparmentsData");
        }

    }
}
