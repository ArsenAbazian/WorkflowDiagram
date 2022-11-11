using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
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

        public T Get<T>(UnitOfWork session, int oid) where T : XPObject {
            var items = new XPCollection<T>(session);
            return items.FirstOrDefault(i => i.Oid == oid);
        }

        public void Add<T>(T item) where T: XPObject {
            if(item.Session != null) {
                ((UnitOfWork)item.Session).CommitChanges();
                return;
            }
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var items = new XPCollection<T>(session);
                items.Add(item);
                session.CommitChanges();
            }
        }

        public UserInfo FindUserByGuidString(string userId) {
            var users = new XPCollection<UserInfo>(XpoDefault.Session);
            return users.FirstOrDefault(u => u.GuidString == userId);
        }

        public UserInfo FindUserByLogin(string normalizedUserName) {
            var users = new XPCollection<UserInfo>(XpoDefault.Session);
            return users.FirstOrDefault(u => u.LoginNormalized == normalizedUserName);
        }

        public UserInfo FindUserByEmail(string normalizedEmail) {
            var users = new XPCollection<UserInfo>(XpoDefault.Session); 
            return users.FirstOrDefault(u => u.EmailNormalized == normalizedEmail);
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
                session.CommitChanges();
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
            UpdateProperty<UserInfo>(oid, nameof(UserInfo.EmailConfirmed), confirmed);
        }

        public string UpdateUserPasswordHash(int oid) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var users = new XPCollection<UserInfo>(session);
                UserInfo info = users.FirstOrDefault(u => u.Oid == oid);
                if(info == null)
                    return string.Empty;
                info.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(info, info.Password);
                session.CommitChanges();
                return info.PasswordHash;
            }
        }

        public void UpdateUserNormalizedLogin(int oid, string normalizedName) {
            UpdateProperty<UserInfo>(oid, nameof(UserInfo.LoginNormalized), normalizedName);
        }

        public void UpdateUserLogin(int oid, string login) {
            UpdateProperty<UserInfo>(oid, nameof(UserInfo.Login), login);
        }

        public void Update<T>(int oid, Action<T> updateAction) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var coll = new XPCollection<T>(session);
                T info = coll.FirstOrDefault(item => ((ISupportId)item).Oid == oid);
                if(info == null)
                    return;
                updateAction(info);
                session.CommitChanges();
            }
        }
        public void UpdateProperty<T>(int oid, string property, object value) {
            using(UnitOfWork session = new UnitOfWork(XpoDefault.DataLayer)) {
                var coll = new XPCollection<T>(session);
                object info = coll.FirstOrDefault(item => ((ISupportId)item).Oid == oid);
                if(info == null)
                    return;
                PropertyInfo pi = info.GetType().GetProperty(property);
                if(pi != null)
                    pi.SetValue(info, value);
                session.CommitChanges();
            }
        }
        internal void UpdateUserNormalizedEmail(int oid, string normalizedEmail) {
            UpdateProperty<UserInfo>(oid, nameof(UserInfo.EmailNormalized), normalizedEmail);
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
                session.CommitChanges();
                return info;
            }
        }
    }
}
