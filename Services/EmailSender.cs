using System.Drawing.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using System.Net;

namespace KursovaWork.Services
{
    public class EmailSender : IEmailSender
    {

        public Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient("smtp.ukr.net", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("baryaroman@ukr.net", "RUA2yFn8oRMAZFAY")
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baryaroman@ukr.net");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            Console.WriteLine("Message was sent successfully...");

            return client.SendMailAsync(mailMessage);
        }
    }
}
