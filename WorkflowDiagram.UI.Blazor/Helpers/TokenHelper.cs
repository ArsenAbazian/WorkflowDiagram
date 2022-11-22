using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public interface ITokenHelper
    {
        string GenerateToken(ITokenOwner owner);
        void AddCookie(HttpContext context, string token);
    }
    public class TokenHelper : ITokenHelper
    {
        private readonly AppSettings _appSettings;

        public TokenHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(ITokenOwner owner)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, owner.GetType().Name + "_" + owner.Oid.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(10 * 365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void AddCookie(HttpContext context, string token) {
            if(Debugger.IsAttached)
                context.Response.Headers.Append("Set-Cookie", $".AspNetCore.App.Id={token}; path=/; SameSite=None; Secure"); //TODO None FOR DEBUG
            else 
                context.Response.Headers.Append("Set-Cookie", $".AspNetCore.App.Id={token}; path=/; SameSite=Lax; Secure"); //TODO Lax FOR RELEASE
        }
    }
}
