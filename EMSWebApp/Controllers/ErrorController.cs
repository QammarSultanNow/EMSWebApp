using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public IActionResult NotFound()
        {
            
            return View();
        }

        [Route("Error/InternalServerError")]
        public IActionResult InternalServerError()
        {

            return View();
        }

        
    }
}
