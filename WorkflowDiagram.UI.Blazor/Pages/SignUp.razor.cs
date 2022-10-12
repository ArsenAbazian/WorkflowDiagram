using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using WorkflowDiagram.UI.Blazor.Helpers;
using WorkflowDiagram.UI.Blazor.ServiceModel;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class SignUp : ComponentBase {
        public SignUp() { }

        [Parameter]
        public string Email { get; set; } = "";
        [Parameter]
        public string Password { get; set; } = "";
        [Parameter]
        public string Login { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public List<string> Errors { get; } = new List<string>();

        async Task OnRegisterClick() {
            if(CheckForErorrs())
                return;

            UserInfo info = DatabaseManager.SignUpUser(Login, Email, Password);
            
            IdentityResult res = await UserManager.CreateAsync(info);
            if(!res.Succeeded) {
                Errors.AddRange(res.Errors.Select(e => e.Description).ToList());
                StateHasChanged();
            }

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(info);
            HttpClient cl = new HttpClient();

            StringContent c = new StringContent(String.Format("{{\"email\":\"{0}\", \"code\": \"{1}\", \"userId\": \"{2}\"}}", info.Email, code, info.GuidString), Encoding.UTF8, "application/json");
            var resp = await cl.PostAsync("https://" + httpContextAccessor.HttpContext.Request.Host.Value + "/api/emailconfirmation/send", c);
            if(resp == null) {
                Errors.Add("Something went wrong while generating confirmation email.");
                StateHasChanged();
                return;
            }

            var body = await resp.Content.ReadAsStringAsync();
            if(body == "true")
                NavManager.NavigateTo("/Account/RegisterConfirmation");
            else {
                Errors.Add("Something went wrong while generating confirmation email.");
                StateHasChanged();
                return;
            }
        }

        private bool CheckForErorrs() {
            Errors.Clear();
            Email = Email.Trim();
            Password = Password.Trim();
            Login = Login.Trim();
            ConfirmPassword = ConfirmPassword.Trim();

            if(DatabaseManager.Current.HasRegisteredUserWithEmail(Email)) {
                Errors.Add("User with specified login already registered. Please use another email.");
                return true;
            }

            if(DatabaseManager.Current.HasRegisteredUserWithLogin(Login)) {
                Errors.Add("User with specified email already registered. Please use another email.");
                return true;
            }

            PasswordInfo info = CheckPassword(Password);
            if(Login.Length < 4)
                Errors.Add("Name length should be greater or equal 4.");
            if(!EmailHelper.IsValid(Email))
                Errors.Add("Invalid email address");
            if(Password.Length < 8)
                Errors.Add("Password length should be greater or equal 8.");
            if(!info.HasDigits)
                Errors.Add("Password should contains at least one digit.");
            if(!info.HasLetters)
                Errors.Add("Password should contains at least one lower-case letter.");
            if(!info.HasUpperCaseLetters)
                Errors.Add("Password should contains at least one upper-case letter.");
            if(!info.HasSymbols)
                Errors.Add("Password should contains at least one symbol like ! ? . { } etc...");
            if(Password != ConfirmPassword)
                Errors.Add("Values in field 'Password' and 'Confirm Password' does not match.");
            if(Errors.Count > 0) {
                StateHasChanged();
                return true;
            }
            return Errors.Count > 0;
        }

        private static PasswordInfo CheckPassword(string password) {
            PasswordInfo info = new ();
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
