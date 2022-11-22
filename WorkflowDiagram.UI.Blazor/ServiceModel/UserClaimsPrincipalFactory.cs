using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class WfUserClaimsPrincipalFactory : Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<UserInfo> {
        public WfUserClaimsPrincipalFactory(UserManager<UserInfo> userManager, IOptions<IdentityOptions> options) : base(userManager, options) { }

        protected override Task<ClaimsIdentity> GenerateClaimsAsync(UserInfo user) {
            var res = base.GenerateClaimsAsync(user);
            return res;
        }
        public override Task<ClaimsPrincipal> CreateAsync(UserInfo user) {
            var res = base.CreateAsync(user);
            return res;
        }
        //Task<ClaimsPrincipal> IUserClaimsPrincipalFactory<UserInfo>.CreateAsync(UserInfo user) {
        //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        //}
    }
}
