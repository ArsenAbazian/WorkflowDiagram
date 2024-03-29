﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class AutenticationUserManager : UserManager<UserInfo> {
        public AutenticationUserManager(IUserStore<UserInfo> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserInfo> passwordHasher, IEnumerable<IUserValidator<UserInfo>> userValidators, IEnumerable<IPasswordValidator<UserInfo>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserInfo>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) {
        }
    }
}
