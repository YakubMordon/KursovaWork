using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace KursovaWork.Services.AdditionalServices
{
    /// <summary>
    /// Клас для відправки електронних листів.
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// Надсилає електронний лист.
        /// </summary>
        /// <param name="mail">Електронна адреса отримувача.</param>
        /// <param name="subject">Тема листа.</param>
        /// <param name="message">Тіло листа.</param>
        public static void SendEmail(string mail, string subject, string message)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress("VAG Dealer", "baryaroman@ukr.net"));
            email.To.Add(new MailboxAddress("Шановний покупець", mail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = message };

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ukr.net", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("baryaroman@ukr.net", "9OyB6M4t9sLWXW8C");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
