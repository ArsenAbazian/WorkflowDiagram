using Microsoft.AspNetCore.Identity;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class AutenticationUserStore : IUserStore<UserInfo>, IUserEmailStore<UserInfo>, IUserPasswordStore<UserInfo> {
        Task<IdentityResult> IUserStore<UserInfo>.CreateAsync(UserInfo user, CancellationToken cancellationToken) {
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
            if(string.IsNullOrEmpty(user.PasswordHash)) {
                return Task.FromResult(DatabaseManager.Current.UpdateUserPasswordHash(user.Oid));
            }
            return Task.FromResult(user.PasswordHash);
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
            throw new NotImplementedException();
        }

        Task IUserEmailStore<UserInfo>.SetEmailConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateEmailConfirmed(user.Oid, confirmed);
            return Task.CompletedTask;
        }

        Task IUserEmailStore<UserInfo>.SetNormalizedEmailAsync(UserInfo user, string normalizedEmail, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateUserNormalizedEmail(user.Oid, normalizedEmail);
            return Task.CompletedTask;
        }

        Task IUserStore<UserInfo>.SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken) {
            DatabaseManager.Current.UpdateUserNormalizedLogin(user.Oid, normalizedName);
            return Task.CompletedTask;
        }

        Task IUserPasswordStore<UserInfo>.SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        Task IUserStore<UserInfo>.SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserStore<UserInfo>.UpdateAsync(UserInfo user, CancellationToken cancellationToken) {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
