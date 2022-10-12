using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Identity;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class DatabaseManager {
        public static DatabaseManager Current { get; private set; }

        public DatabaseManager(string connectionString) {
            SetDatabase(connectionString, AutoCreateOption.DatabaseAndSchema);
            Current = this;
        }

        public IDataLayer DataLayer { get; set; }
        public IDataStore DataStore { get; set; }

        public void SetDatabase(string connectionString, AutoCreateOption autoCreateOption) {
#pragma warning disable CS0618 // Type or member is obsolete
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;
#pragma warning restore CS0618 // Type or member is obsolete
            try {
                DataLayer = XpoDefault.GetDataLayer(connectionString, autoCreateOption);
                DataStore = XpoDefault.GetConnectionProvider(connectionString, autoCreateOption);
            }
            catch(Exception) {

            }

            XpoDefault.DataLayer = DataLayer;
            XpoDefault.Session = new UnitOfWork(DataLayer);


            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                InitializeDefaultSystemUsers(session);
                session.CommitChanges();
            }
        }

        public UserInfo FindUserByGuidString(string userId) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                return users.FirstOrDefault(u => u.GuidString == userId);
            }
        }

        public UserInfo FindUserByLogin(string normalizedUserName) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                return users.FirstOrDefault(u => u.LoginNormalized == normalizedUserName);
            }
        }

        public UserInfo FindUserByEmail(string normalizedEmail) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                return users.FirstOrDefault(u => u.EmailNormalized == normalizedEmail);
            }
        }

        public bool CompleteRegistrationFor(string guidString) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.GuidString == guidString);
                if(info == null)
                    return false;
                info.NeedCompleteRegistration = false;
                info.EmailConfirmed = true;
                info.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(info, info.Password);
                session.CommitTransaction();
                return true;
            }
        }

        public UserInfo LoginUser(string email, string password) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if(info == null || info.NeedCompleteRegistration)
                    return info;
                return info;
            }
        }

        public void UpdateEmailConfirmed(int oid, bool confirmed) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Oid == oid);
                if(info == null)
                    return ;
                info.EmailConfirmed = confirmed;
                session.CommitTransaction();
            }
        }

        public string UpdateUserPasswordHash(int oid) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Oid == oid);
                if(info == null)
                    return string.Empty;
                info.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(info, info.Password);
                session.CommitTransaction();
                return info.PasswordHash;
            }
        }

        internal void UpdateUserNormalizedLogin(int oid, string normalizedName) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Oid == oid);
                if(info == null)
                    return;
                info.LoginNormalized = normalizedName;
                session.CommitTransaction();
            }
        }

        internal void UpdateUserNormalizedEmail(int oid, string normalizedEmail) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Oid == oid);
                if(info == null)
                    return;
                info.EmailNormalized = normalizedEmail;
                session.CommitTransaction();
            }
        }

        private void InitializeDefaultSystemUsers(UnitOfWork session) {
            XPCollection<SystemUserInfo> coll = new XPCollection<SystemUserInfo>(session);
            if(coll.Count == 0) {
                SystemUserInfo prog = new SystemUserInfo(session) { Login = "prog", Name = "Programmmer", LastName = "", Password = "programmer", Type = SystemUserType.Programmer };
                session.Save(prog);

                SystemUserInfo admin = new SystemUserInfo(session) { Login = "admin", Name = "Administrator", LastName = "", Password = "admin", Type = SystemUserType.Administrator };
                session.Save(admin);
            }
        }

        public bool HasRegisteredUserWithEmail(string email) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                return users.FirstOrDefault(u => u.Email == email && !u.NeedCompleteRegistration) != null;
            }
        }

        public bool HasRegisteredUserWithLogin(string login) {
            login = login.ToUpperInvariant();
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                return users.FirstOrDefault(u => u.LoginNormalized == login && !u.NeedCompleteRegistration) != null;
            }
        }

        public UserInfo SignUpUser(string login, string email, string password) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);

                bool addNewUser = false;
                UserInfo info = users.FirstOrDefault(u => u.Email == email && u.NeedCompleteRegistration);
                if(info == null) {
                    info = new UserInfo(session);
                    addNewUser = true;
                }

                info.Login = login;
                info.LoginNormalized = login.ToUpperInvariant();
                info.Email = email;
                info.EmailNormalized = email.ToUpperInvariant();
                info.Password = password;
                info.NeedCompleteRegistration = true;
                if(addNewUser)
                    users.Add(info);
                session.CommitTransaction();
                return info;
                //await new EmailHelper().SendEmailAsync(
                //    info.Email, 
                //    "Complete Registration", 
                //    string.Format("Dear user!\n\n Please complete registration by clicking the following link: localhost:7156/completeregistration={0}.\n\nThanks,\nLowCode Team.", info.GuidString));
            }
        }
    }
}
