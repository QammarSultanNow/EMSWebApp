using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Azure.Core;
using EMSWebApp.Services;
using Hangfire;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;

namespace EMSWebApp.Controllers
{
    public class JobController : ControllerBase
    {
       private readonly IEmailService _emailService;
       private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobController(IEmailService emailService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }
        public IActionResult RecurringJobs()
        {
            RecurringJob.AddOrUpdate(() => _emailService.SendMail("Recurring_Jobs", DateTime.Now.ToLongTimeString()),Cron.Hourly);
            return Ok("Scheduled jobs are done in 2 minutes");
        }
        public IActionResult GetFireAndForgetJob()
        {
            BackgroundJob.Schedule(() => _emailService.SendMail("Fire-and-forget-job", DateTime.Now.ToLongTimeString()), TimeSpan.FromMinutes(2));
            return Ok("Scheduled jobs are done in 2 minutes");
        }
    }
}
    