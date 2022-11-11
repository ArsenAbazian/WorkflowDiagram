using DevExpress.Xpo;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class UserInfo : XPObject, IUserInfo, ITokenOwner, ISupportId {
        public UserInfo() : base() {
            GuidString = System.Guid.NewGuid().ToString();
        }
        public UserInfo(Session session) : base(session) {
            GuidString = System.Guid.NewGuid().ToString();
        }

        public bool IsSystemUser { get { return false; } }
        UserType IUserInfo.UserType { get { return Type; } }
        SystemUserType IUserInfo.SystemUserType { get { return SystemUserType.User; } }

        string passwordHash;
        [DisplayName("PasswordHash")]
        public string PasswordHash {
            get { return passwordHash; }
            set { SetPropertyValue<string>(nameof(PasswordHash), ref passwordHash, value); }
        }

        string guid;
        [DisplayName("GuidString")]
        public string GuidString {
            get { return guid; }
            set { SetPropertyValue<string>(nameof(GuidString), ref guid, value); }
        }


        string emailNormalized;
        [DisplayName("EmailNormalized")]
        public string EmailNormalized {
            get { return emailNormalized; }
            set { SetPropertyValue<string>(nameof(EmailNormalized), ref emailNormalized, value); }
        }

        string loginNormalized;
        [DisplayName("LoginNormalized")]
        public string LoginNormalized {
            get { return loginNormalized; }
            set { SetPropertyValue<string>(nameof(LoginNormalized), ref loginNormalized, value); }
        }

        string login;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login must be specified")]
        [MinLength(4, ErrorMessage = "Login length must be greater than 4 and less than 32 characters")]
        [MaxLength(32, ErrorMessage = "Login length must be greater than 4 and less than 32 characters")]
        [DisplayName("Login")]
        [Size(32)]
        public string Login {
            get { return login; }
            set { SetPropertyValue<string>("Login", ref login, value); }
        }

        string password;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password must be specified")]
        [MinLength(8, ErrorMessage = "Password length must be greater than 8 and less than 32 characters")]
        [MaxLength(32, ErrorMessage = "Password length must be greater than 8 and less than 32 characters")]
        [DisplayName("Password")]
        [Size(32)]
        public string Password {
            get { return password; }
            set { SetPropertyValue<string>("Password", ref password, value); }
        }

        string name;
        [DisplayName("Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be specified")]
        [Size(128)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }

        string lastName;
        [DisplayName("Second name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Second name must be specified")]
        [Size(128)]
        public string LastName {
            get { return lastName; }
            set { SetPropertyValue<string>(nameof(LastName), ref lastName, value); }
        }

        UserType type;
        [DisplayName("Role")]
        public UserType Type {
            get { return type; }
            set { SetPropertyValue<UserType>("Type", ref type, value); }
        }

        string email;
        [DisplayName("E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail must be specified")]
        [EmailAddress(ErrorMessage = "E-mail is incorrect")]
        [Size(256)]
        public string Email {
            get { return email; }
            set { SetPropertyValue<string>(nameof(Email), ref email, value); }
        }

        string phone;
        [DisplayName("Phone")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone must be specified")]
        [Phone(ErrorMessage = "Phone number is incorrect")]
        [Size(32)]
        public string Phone {
            get { return phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref phone, value); }
        }

        bool phoneConfirmed;
        [DisplayName("Phone confirmed")]
        public bool PhoneConfirmed {
            get { return phoneConfirmed; }
            set { SetPropertyValue<bool>(nameof(PhoneConfirmed), ref phoneConfirmed, value); }
        }

        bool emailConfirmed;
        [DisplayName("E-mail confirmed")]
        public bool EmailConfirmed {
            get { return emailConfirmed; }
            set { SetPropertyValue<bool>(nameof(EmailConfirmed), ref emailConfirmed, value); }
        }

        string smsNumber;
        [Size(32)]
        public string SmsNumber {
            get { return smsNumber; }
            set { SetPropertyValue<string>(nameof(SmsNumber), ref smsNumber, value); }
        }

        string smsCode;
        [Size(32)]
        public string SmsCode {
            get { return smsCode; }
            set { SetPropertyValue<string>(nameof(SmsCode), ref smsCode, value); }
        }

        string emailCode;
        [Size(32)]
        public string EmailCode {
            get { return emailCode; }
            set { SetPropertyValue<string>(nameof(EmailCode), ref emailCode, value); }
        }

        string emailNumber;
        [Size(32)]
        public string EmailNumber {
            get { return emailNumber; }
            set { SetPropertyValue<string>(nameof(EmailNumber), ref emailNumber, value); }
        }

        string token;
        [Size(512)]
        public string Token {
            get { return token; }
            set { SetPropertyValue<string>("Token", ref token, value); }
        }

		bool cookiesAccepted;
		[DisplayName("Cookies Accepted")]
		public bool CookiesAccepted {
			get { return cookiesAccepted; }
			set { SetPropertyValue<bool>(nameof(CookiesAccepted), ref cookiesAccepted, value); }
		}
        public string FullName { get { return LastName + " " + Name; } }

        [DisplayName("Workspaces")]
        [DevExpress.Xpo.Association("Owner-Workspaces")]
        public XPCollection<WorkspaceInfo> Workspaces {
            get { return GetCollection<WorkspaceInfo>(nameof(Workspaces)); }
        }

        bool needCompleteRegistration;
        [DisplayName("NeedCompleteRegistration")]
        public bool NeedCompleteRegistration {
            get { return needCompleteRegistration; }
            set { SetPropertyValue<bool>(nameof(NeedCompleteRegistration), ref needCompleteRegistration, value); }
        }
    }

    public enum UserType {
        [System.ComponentModel.Description("User")]
        Default = 0,
        [System.ComponentModel.Description("Owner")]
        Owner = 1,
        [System.ComponentModel.Description("Administrator")]
        Admin = 5
    }
}
