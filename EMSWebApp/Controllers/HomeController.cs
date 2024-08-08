using ApplicationCore.Interfaces;
using ApplicationCore.UseCases.Departments.CountTotals;
using EMSWebApp.Interface;
using EMSWebApp.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace EMSWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;
      //  private readonly IDepartmentRepository _departmentRepository;
      //  private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IStringLocalizer<HomeController> localizer, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
          //  _departmentRepository = departmentRepository;
          //  _employeeRepository = employeeRepository;
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
            var result = await _mediator.Send(new CountTotalsRequest());
           return Ok(result);
        }

        public IActionResult chartView()
        {
            return View();
        }
    }
}
