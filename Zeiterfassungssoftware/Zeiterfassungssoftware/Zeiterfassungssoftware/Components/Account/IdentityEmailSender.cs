using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Zeiterfassungssoftware.Data;

namespace Zeiterfassungssoftware.Components.Account
{
    // Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
    internal sealed class IdentityEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender EmailSender = new EmailSender();

        public Task SendConfirmationLinkAsync(ApplicationUser User, string Email, string ConfirmationLink) =>
            EmailSender.SendEmailAsync(Email, "Confirm your Email", $"Please confirm your account by <a href='{ConfirmationLink}'>clicking here</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser User, string Email, string ResetLink) =>
            EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password by <a href='{ResetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser User, string Email, string ResetCode) =>
            EmailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password using the following code: {ResetCode}");
    }
}
