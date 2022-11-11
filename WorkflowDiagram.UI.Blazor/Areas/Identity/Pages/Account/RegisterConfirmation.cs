using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Areas.Identity.Pages.Account {
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel {
        public string Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string EmailConfirmationUrl { get; set; }

        private readonly UserManager<UserInfo> userManager;
        private readonly IEmailSender sender;

        public RegisterConfirmationModel(UserManager<UserInfo> userManager, IEmailSender sender) {
            this.userManager = userManager;
            this.sender = sender;
        }

        public virtual async Task<IActionResult> OnGetAsync(string email, string returnUrl = null) {
            if(email == null) {
                return RedirectToPage("/Index");
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await this.userManager.FindByEmailAsync(email);
            if(user == null) {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            //// If the email sender is a no-op, display the confirm link in the page
            //DisplayConfirmAccountLink = this.sender is EmailSender;
            //if(DisplayConfirmAccountLink) {
                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            //}

            return Page();
        }
    }
}
