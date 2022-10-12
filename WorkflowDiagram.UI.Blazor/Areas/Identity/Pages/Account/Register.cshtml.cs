using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public string Login { get; set; } = "";

        public void OnGet() {
        }

        async Task OnRegisterClick2() {
        //    if(CheckForErorrs())
        //        return;

              UserInfo info = DatabaseManager.Current.LoginUser(Email, Password);
            //    IdentityResult res = await UserManager.CreateAsync(info);
            //    if(!res.Succeeded) {
            //        Errors.AddRange(res.Errors.Select(e => e.Description).ToList());
            //        //StateHasChanged();
            //    }

            //var code = await UserManager.GenerateEmailConfirmationTokenAsync(info);
            string code = "hello";
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = info.GuidString, code = code },
                protocol: Request.Scheme);

        //    await _emailSender.SendEmailAsync(info.Email, "Confirm your email",
        //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            RedirectToPage("RegisterConfirmation", new { email = Email });
        //    //NavManager.NavigateTo("/aftersignup");
        }
    }
}
