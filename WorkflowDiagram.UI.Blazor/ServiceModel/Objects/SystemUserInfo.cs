using DevExpress.Xpo;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public interface IUserInfo {
        int Oid { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string Token { get; set; }
        bool IsSystemUser { get; }
        UserType UserType { get; }
        SystemUserType SystemUserType { get; }
    }

    public class SystemUserInfo : XPObject, IUserInfo, ITokenOwner, ISupportId {
        public SystemUserInfo(Session session) : base(session) { }         

        public bool IsSystemUser { get { return true; } }
        UserType IUserInfo.UserType { get { return UserType.Default; } }
        SystemUserType IUserInfo.SystemUserType { get { return Type; } }

        string login;
        [Size(32)]
        public string Login {
            get { return login; }
            set { SetPropertyValue<string>("Login", ref login, value); }
        }

        string password;
        [Size(32)]
        public string Password {
            get { return password; }
            set { SetPropertyValue<string>("Password", ref password, value); }
        }

        string name;
        [Size(128)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }

        string lastName;
        [Size(128)]
        public string LastName {
            get { return lastName; }
            set { SetPropertyValue<string>("LastName", ref lastName, value); }
        }

        SystemUserType type;
        public SystemUserType Type {
            get { return type; }
            set { SetPropertyValue<SystemUserType>("Type", ref type, value); }
        }

        string token;
        [Size(512)]
        public string Token {
            get { return token; }
            set { SetPropertyValue<string>("Token", ref token, value); }
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

        bool phoneApproved;
        [DisplayName("Phone approved")]
        public bool PhoneApproved {
            get { return phoneApproved; }
            set { SetPropertyValue<bool>(nameof(PhoneApproved), ref phoneApproved, value); }
        }

        bool emailApproved;
        [DisplayName("E-mail approved")]
        public bool EmailApproved {
            get { return emailApproved; }
            set { SetPropertyValue<bool>(nameof(EmailApproved), ref emailApproved, value); }
        }

        int smsNumber;
        public int SmsNumber {
            get { return smsNumber; }
            set { SetPropertyValue<int>(nameof(SmsNumber), ref smsNumber, value); }
        }

        int smsCode;
        public int SmsCode {
            get { return smsCode; }
            set { SetPropertyValue<int>(nameof(SmsCode), ref smsCode, value); }
        }

        int emailCode;
        public int EmailCode {
            get { return emailCode; }
            set { SetPropertyValue<int>(nameof(EmailCode), ref emailCode, value); }
        }

        int emailNumber;
        public int EmailNumber {
            get { return emailNumber; }
            set { SetPropertyValue<int>(nameof(EmailNumber), ref emailNumber, value); }
        }
    }

    public enum SystemUserType {
        [System.ComponentModel.Description("Developer")]
        Programmer = 0,
        [System.ComponentModel.Description("Administrator")]
        Administrator = 1,
        [System.ComponentModel.Description("User")]
        User = 2
    }
}
