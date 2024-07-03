using EMSWebApp.Data;
using EMSWebApp.Interface;
using EMSWebApp.Models;
using EMSWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSWebApp.Component
{
    public class EmployeeViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeViewComponent(IEmployeeRepository repository)
        {
            _repository = repository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
           var result = await _repository.DepartmentEmployeeCounting();
            return View(result);
        }
    }
}
