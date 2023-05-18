using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.AdditionalServices
{
    /// <summary>
    /// Клас для відправки електронних листів.
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        /// 
        private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger(typeof(EmailSender));
        /// <summary>
        /// Надсилає електронний лист.
        /// </summary>
        /// <param name="mail">Електронна адреса отримувача.</param>
        /// <param name="subject">Тема листа.</param>
        /// <param name="message">Тіло листа.</param>
        public static void SendEmail(string mail, string subject, string message)
        {
            _logger.LogInformation("Вхід в метод надсилання листа");

            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress("VAG Dealer", "baryaroman@ukr.net"));
            email.To.Add(new MailboxAddress("Шановний покупець", mail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = message };

            _logger.LogInformation("Дані для надсилання встановленні");

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ukr.net", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("baryaroman@ukr.net", "9OyB6M4t9sLWXW8C");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            _logger.LogInformation("Лист був успішно надісланий");
        }
    }
}
