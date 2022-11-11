using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using WorkflowDiagram.UI.Blazor.Helpers;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public class InputModel {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public string ConfirmPassword { get; set; } = "";
            public string Login { get; set; } = "";
            public List<string> Errors { get; } = new List<string>();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        private readonly SignInManager<UserInfo> signInManager;
        private readonly UserManager<UserInfo> userManager;
        private readonly IUserStore<UserInfo> userStore;
        private readonly IUserEmailStore<UserInfo> emailStore;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<UserInfo> userManager,
            IUserStore<UserInfo> userStore,
            SignInManager<UserInfo> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender) {
            this.userManager = userManager;
            this.userStore = userStore;
            this.emailStore = GetEmailStore();
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public string ReturnUrl { get; set; }

        public virtual void OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        private IUserEmailStore<UserInfo> GetEmailStore() {
            if(!this.userManager.SupportsUserEmail) {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UserInfo>)userStore;
        }

        public virtual async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            returnUrl ??= Url.Content("~/");
            if(CheckForErorrs()) {
                foreach(var error in Input.Errors) {
                    ModelState.AddModelError(string.Empty, error);
                }
                return Page();
            }
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if(ModelState.IsValid) {
                var user = new UserInfo();

                await this.userStore.SetUserNameAsync(user, Input.Login, CancellationToken.None);
                await this.emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await this.userManager.CreateAsync(user, Input.Password);

                if(result.Succeeded) {
                    this.logger.LogInformation("User created a new account with password.");

                    var userId = await this.userManager.GetUserIdAsync(user);
                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //HtmlEncoder.Default.Encode(callbackUrl)
                    await new EmailHelper().SendEmailAsync(Input.Email, "Confirm your email",
                        $"<html>" +
                        $"  <body>" +
                        $"      Please confirm your account by <a href='{callbackUrl}'>clicking here</a>." +
                        $"  </body>" +
                        $"<html>");

                    if(this.userManager.Options.SignIn.RequireConfirmedAccount) {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach(var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private bool CheckForErorrs() {
            Input.Errors.Clear();
            Input.Email = Input.Email.Trim();
            Input.Password = Input.Password.Trim();
            Input.Login = Input.Login.Trim();
            Input.ConfirmPassword = Input.ConfirmPassword.Trim();

            if(DatabaseManager.Current.HasRegisteredUserWithEmail(Input.Email)) {
                Input.Errors.Add("User with specified email already registered. Please use another email.");
                return true;
            }

            if(DatabaseManager.Current.HasRegisteredUserWithLogin(Input.Login)) {
                Input.Errors.Add("User with specified login already registered. Please use another email.");
                return true;
            }

            PasswordInfo info = CheckPassword(Input.Password);
            if(Input.Login.Length < 4)
                Input.Errors.Add("Name length should be greater or equal 4.");
            if(!EmailHelper.IsValid(Input.Email))
                Input.Errors.Add("Invalid email address");
            if(Input.Password.Length < 8)
                Input.Errors.Add("Password length should be greater or equal 8.");
            if(!info.HasDigits)
                Input.Errors.Add("Password should contains at least one digit.");
            if(!info.HasLetters)
                Input.Errors.Add("Password should contains at least one lower-case letter.");
            if(!info.HasUpperCaseLetters)
                Input.Errors.Add("Password should contains at least one upper-case letter.");
            if(!info.HasSymbols)
                Input.Errors.Add("Password should contains at least one symbol like ! ? . { } etc...");
            if(Input.Password != Input.ConfirmPassword)
                Input.Errors.Add("Values in field 'Password' and 'Confirm Password' does not match.");
            
            return Input.Errors.Count > 0;
        }

        private static PasswordInfo CheckPassword(string password) {
            PasswordInfo info = new();
            for(int i = 0; i < password.Length; i++) {
                char c = password[i];
                info.HasUpperCaseLetters |= char.IsUpper(c) && char.IsLetter(c);
                info.HasDigits |= char.IsDigit(c);
                info.HasLetters |= char.IsLower(c) && char.IsLetter(c);
                info.HasSymbols |= char.IsPunctuation(c);
            }
            return info;
        }
    }

    internal class PasswordInfo {
        public bool HasLetters { get; set; }
        public bool HasDigits { get; set; }
        public bool HasSymbols { get; set; }
        public bool HasUpperCaseLetters { get; set; }

        public bool IsValid { get { return HasLetters && HasDigits && HasSymbols && HasUpperCaseLetters; } }
    }
}
