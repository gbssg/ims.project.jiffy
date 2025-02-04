using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Zeiterfassungssoftware.Data;

namespace Zeiterfassungssoftware.Components.Account
{
    // Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
    internal sealed class IdentityEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender EmailSender = new EmailSender();

        public Task SendConfirmationLinkAsync(ApplicationUser User, string Email, string ConfirmationLink)
        {
            var Subject = "Verify your email";
            var Body = @"
                <!DOCTYPE html>
                <html lang=""en"">

                <head>
                    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
                    <title>
                        Confirm Your Email
                    </title>
                    <style type=""text/css"">
                        body {
                          background-color: #191919;
                          color: #fff;
                          font-family: Arial, Helvetica, sans-serif;
                          margin: 0;
                          padding: 0;
                          display: flex;
                          justify-content: center;
                          align-items: center;
                          min-height: 100vh
                        }
      
                        .container {
                          background-color: #292929;
                          padding: 20px 30px;
                          border-radius: 12px;
                          max-width: 400px;
                          text-align: center;
                          box-shadow: 0 4px 8px rgba(0, 0, 0, .5)
                        }
      
                        .title {
                          font-weight: 700;
                          font-size: 28px;
                          color: #cc0;
                          margin: 0;
                          padding: 10px 0
                        }
      
                        .message {
                          font-size: 16px;
                          line-height: 1.6;
                          color: #e0e0e0;
                          margin: 15px 0 20px
                        }
      
                        .button {
                          display: inline-block;
                          padding: 15px 35px;
                          border-radius: 8px;
                          background: #bb86fc;
                          border: none;
                          color: #191919;
                          font-weight: 700;
                          font-size: 16px;
                          text-decoration: none;
                          cursor: pointer;
                          transition: background .3s ease, transform .2s ease
                        }
      
                        .button:hover {
                          background: #e6e600
                        }
      
                        .button:active {
                          transform: scale(.95)
                        }
      
                        .footer {
                          font-size: 14px;
                          color: #888;
                          margin-top: 20px
                        }
                      </style>
                </head>

                <body>
                      <div class=""container"">
                        <p class=""title"">Jiffy
                        <p class=""message"">Thank you for joining Jiffy! <br>
                          <br>To complete your registration, please confirm your email address by clicking the button below.
                        </p>
                        <a class=""button"" href=""{ConfirmationLink}"">Confirm Email</a>
                        <p class=""footer"">
                          <br>If you didn’t sign up for this account, please ignore this email.
                      </div>
                </body>

                </html>
            ".Replace("{ConfirmationLink}", ConfirmationLink);
            return EmailSender.SendEmailAsync(Email, Subject, Body);
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser User, string Email, string ResetLink) =>
            EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password by <a href='{ResetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser User, string Email, string ResetCode) =>
            EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password using the following code: {ResetCode}");
    }
}
