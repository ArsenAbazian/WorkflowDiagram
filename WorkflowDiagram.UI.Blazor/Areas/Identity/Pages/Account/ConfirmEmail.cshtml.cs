using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<UserInfo> userManager;

        public ConfirmEmailModel(UserManager<UserInfo> userManager) {
            this.userManager = userManager;
        }

        public string StatusMessage { get; set; }

        public virtual async Task<IActionResult> OnGetAsync(string userId, string code) {
            if(userId == null || code == null) {
                return RedirectToPage("/Index");
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if(user == null) {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email. You can now login by clicking Login link in upper right corner." : "Error confirming your email.";
            return Page();
        }
    }
}
