using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace KursovaWork.Services
{
    public class EmailSender
    {
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
