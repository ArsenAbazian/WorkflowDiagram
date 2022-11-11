using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkflowDiagram.UI.Blazor.Helpers;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel {
        public class InputModel {
            public bool RememberMe { get; set; } = false;
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public List<string> Errors { get; } = new List<string>();
        }

        private readonly SignInManager<UserInfo> signInManager;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(SignInManager<UserInfo> signInManager, ILogger<LoginModel> logger) {
            this.signInManager = signInManager;
            this.logger = logger;
        }
        
        public string ReturnUrl { get; set; }

        public virtual void OnGet(string returnUrl = null) {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public virtual async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");

            //ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if(ModelState.IsValid) {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded) {
                    this.logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if(result.RequiresTwoFactor) {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if(result.IsLockedOut) {
                    logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
