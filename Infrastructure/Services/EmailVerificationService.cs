using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace EMSWebApp.Services
{
    public class EmailVerificationService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("bioprobs@gmail.com");
            mail.Subject = subject;
            string Body = htmlMessage;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("bioprobs@gmail.com", "gwlz dpzb jrgq dgiz"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(mail);
                smtp.Dispose();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }

        }
    }
}
