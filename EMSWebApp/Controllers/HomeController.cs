using ApplicationCore.Interfaces;
using EMSWebApp.Interface;
using EMSWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace EMSWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
          

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("Home/GetDepartmentData")]
        public async Task<IActionResult> GetDepartmentData()
        {
           var result = await _departmentRepository.EmployeeCount();
           return Ok(result);
        }

        public IActionResult chartView()
        {
            return View();
        }
    }
}
