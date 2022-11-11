using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.ServiceModel.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmailConfirmationController : ControllerBase {
        public EmailConfirmationController() { }

        [HttpGet]
        public string Get() {
            return "Hello from EmailConfirmationController";
        }

        [HttpPost("send")]
        public async Task<ActionResult<bool>> Post(ConfirmationInfo info) {
            try {

                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(info.Code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = info.UserId, code = code },
                    protocol: HttpContext.Request.Scheme);
            
                    await new EmailHelper().SendEmailAsync(info.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }
            catch(Exception) { 
                return new ActionResult<bool>(false);
            }
            return new ActionResult<bool>(true);
        }
    }

    public class ConfirmationInfo {
        public string Email { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
