using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Helpers.Services.Contracts;

namespace Helpers.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var from = new MailAddress("ggmon007@gmail.com", "WEBAPI");
                var to = new MailAddress(email);

                var smtpMessage = new MailMessage(from, to);

                smtpMessage.Subject = subject;
                smtpMessage.Body = message;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials =
                    new NetworkCredential("ggmon007@gmail.com", "0000000000");

                smtpClient.Send(smtpMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
