using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Zeiterfassungssoftware.Components.Account
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string receiver, string subject, string htmlMessage)
        {
            MailAddress to = new MailAddress(receiver);
            MailAddress from = new MailAddress("jiffy.gbs@niederer.swiss");

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = htmlMessage;
            email.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.servicehoster.ch";
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential("jiffy.gbs@niederer.swiss", "smtp_password");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (Exception ex) { }
        }
    }
}
