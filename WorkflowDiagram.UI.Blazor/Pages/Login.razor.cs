using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using WorkflowDiagram.UI.Blazor.Helpers;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class Login {
        public Login() { }

        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public List<string> Errors { get; } = new List<string>();

        async Task OnLoginClick() {
            if(CheckForErorrs())
                return;

            UserInfo info = DatabaseManager.LoginUser(Email, Password);
            if(info == null) {
                Errors.Add("There is no user with specified email and password.");
                StateHasChanged();
                return;
            }
            SignInResult res = await SignInManager.CheckPasswordSignInAsync(info, Password, true);
            if(res.IsNotAllowed || info.NeedCompleteRegistration) {
                NavManager.NavigateTo(string.Format("/completeregistration?{0}", info.GuidString));
            }
            if(!res.Succeeded) {
                Errors.Add("SignIn failed.");
                StateHasChanged();
                return;
            }
            await SignInManager.SignInAsync(info, true);

            NavManager.NavigateTo("/userboard");
        }

        void OnForgotPasswordClick() {

        }

        private bool CheckForErorrs() {
            Errors.Clear();
            Email = Email.Trim();
            Password = Password.Trim();

            if(!EmailHelper.IsValid(Email))
                Errors.Add("Invalid email address");
            if(Password.Length < 8)
                Errors.Add("Password length should be greater or equal 8.");
            if(Errors.Count > 0) {
                StateHasChanged();
                return true;
            }
            return Errors.Count > 0;
        }
    }
}
