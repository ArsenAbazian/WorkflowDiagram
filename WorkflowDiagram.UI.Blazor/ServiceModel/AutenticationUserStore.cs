using Microsoft.AspNetCore.Identity;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class AutenticationUserStore : IUserStore<UserInfo>, IUserEmailStore<UserInfo>, IUserPasswordStore<UserInfo>, IUserPhoneNumberStore<UserInfo> {
        Task<IdentityResult> IUserStore<UserInfo>.CreateAsync(UserInfo user, CancellationToken cancellationToken) {
            DatabaseManager.Current.Add<UserInfo>(user);
            return Task.FromResult(IdentityResult.Success);
        }

        Task<IdentityResult> IUserStore<UserInfo>.DeleteAsync(UserInfo user, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose() {
            
        }

        Task<UserInfo> IUserEmailStore<UserInfo>.FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) {
            return Task.FromResult(DatabaseManager.Current.FindUserByEmail(normalizedEmail));
        }

        Task<UserInfo> IUserStore<UserInfo>.FindByIdAsync(string userId, CancellationToken cancellationToken) {
            return Task.FromResult(DatabaseManager.Current.FindUserByGuidString(userId));
        }

        Task<UserInfo> IUserStore<UserInfo>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            return Task.FromResult(DatabaseManager.Current.FindUserByLogin(normalizedUserName));
        }

        Task<string> IUserEmailStore<UserInfo>.GetEmailAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Email);
        }

        Task<bool> IUserEmailStore<UserInfo>.GetEmailConfirmedAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.EmailConfirmed);
        }

        Task<string> IUserEmailStore<UserInfo>.GetNormalizedEmailAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Email);
        }

        Task<string> IUserStore<UserInfo>.GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.LoginNormalized);
        }

        Task<string> IUserPasswordStore<UserInfo>.GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken) {
            if(string.IsNullOrEmpty(user.PasswordHash))
                return Task.FromResult(new PasswordHasher<UserInfo>().HashPassword(user, user.Password));
            return Task.FromResult(user.PasswordHash);
        }

        Task<string> IUserPhoneNumberStore<UserInfo>.GetPhoneNumberAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Phone);
        }

        Task<bool> IUserPhoneNumberStore<UserInfo>.GetPhoneNumberConfirmedAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.PhoneConfirmed);
        }

        Task<string> IUserStore<UserInfo>.GetUserIdAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.GuidString);
        }

        Task<string> IUserStore<UserInfo>.GetUserNameAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Login);
        }

        Task<bool> IUserPasswordStore<UserInfo>.HasPasswordAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        Task IUserEmailStore<UserInfo>.SetEmailAsync(UserInfo user, string email, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateProperty<UserInfo>(user.Oid, nameof(UserInfo.Email), email);
            user.Email = email;
            return Task.CompletedTask;
        }

        Task IUserEmailStore<UserInfo>.SetEmailConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateEmailConfirmed(user.Oid, confirmed);
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        Task IUserEmailStore<UserInfo>.SetNormalizedEmailAsync(UserInfo user, string normalizedEmail, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateUserNormalizedEmail(user.Oid, normalizedEmail);
            user.EmailNormalized = normalizedEmail;
            return Task.CompletedTask;
        }

        Task IUserStore<UserInfo>.SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateUserNormalizedLogin(user.Oid, normalizedName);
            user.LoginNormalized = normalizedName;
            return Task.CompletedTask;
        }

        Task IUserPasswordStore<UserInfo>.SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken) {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        Task IUserPhoneNumberStore<UserInfo>.SetPhoneNumberAsync(UserInfo user, string phoneNumber, CancellationToken cancellationToken) {
            user.Phone = phoneNumber;
            user.PhoneConfirmed = false;
            DatabaseManager.Current.Update<UserInfo>(user.Oid, item => { item.Phone = phoneNumber; item.PhoneConfirmed = false; });
            return Task.CompletedTask;
        }

        Task IUserPhoneNumberStore<UserInfo>.SetPhoneNumberConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken) {
            user.PhoneConfirmed = confirmed;
            DatabaseManager.Current.Update<UserInfo>(user.Oid, item => { item.PhoneConfirmed = confirmed; });
            return Task.CompletedTask;
        }

        Task IUserStore<UserInfo>.SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateUserLogin(user.Oid, userName);
            user.Login = userName;
            return Task.CompletedTask;
        }

        Task<IdentityResult> IUserStore<UserInfo>.UpdateAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
