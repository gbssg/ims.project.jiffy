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
            var From = new MailAddress(Builder.Configuration["EmailAddress"]);

            var Email = new MailMessage(From, To)
            {
                Subject = Subject,
                IsBodyHtml = true,
                Body = HtmlMessage,
                
            };

            var Smtp = new SmtpClient()
            {
                Host = "smtp.servicehoster.ch",
                Port = 25,
                Credentials = new NetworkCredential(Builder.Configuration["EmailAddress"], Builder.Configuration["EmailPass"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            try
            {
                await Smtp.SendMailAsync(Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
