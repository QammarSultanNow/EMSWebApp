using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateBackGround()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Background job triggered"));
            return Ok();
        }

        public IActionResult scheduleTimeBG()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            BackgroundJob.Schedule(() => Console.WriteLine("Scedule Date Time Trigger") , dateTimeOffset);
            return Ok();
        }
    }
}
    