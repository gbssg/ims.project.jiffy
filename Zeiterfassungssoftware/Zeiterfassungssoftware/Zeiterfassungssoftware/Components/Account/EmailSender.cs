using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Zeiterfassungssoftware.Components.Account
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string Receiver, string Subject, string HtmlMessage)
        {
            var Builder = WebApplication.CreateBuilder();

            var To = new MailAddress(Receiver);
            var From = new MailAddress("jiffy.gbs@niederer.swiss");

            var Email = new MailMessage(From, To);
            Email.Subject = Subject;
            Email.Body = HtmlMessage;
            Email.IsBodyHtml = true;

            var Smtp = new SmtpClient();
            Smtp.Host = "smtp.servicehoster.ch";
            Smtp.Port = 25;
            Smtp.Credentials = new NetworkCredential("jiffy.gbs@niederer.swiss", Builder.Configuration["EmailPass"]);
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;

            try
            {
                Smtp.Send(Email);
            }
            catch (Exception ex) { }
        }
    }
}
