using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public void SendMail(string jobType, string startTime)
        {
            Console.WriteLine(jobType + "-" + startTime + "- Email Successfull Sent-" + DateTime.Now.ToLongTimeString());
        }

       
    }
}
