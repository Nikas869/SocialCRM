using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SocialCRM.Server.Core.Interfaces;
using SocialCRM.Dtos.Models;

namespace SocialCRM.Server.Api.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserAccountService userAccountService;
        private readonly IUserService userService;

        public AccountController(IUserAccountService userAccountService, IUserService userService)
        {
            this.userAccountService = userAccountService;
            this.userService = userService;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterDto userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await userAccountService.RegisterAsync(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [Authorize]
        [Route("GetInfo")]
        public async Task<UserDto> GetInfo()
        {
            var user = await userService.GetByIdAsync(User.Identity.GetUserId());

            return user;
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
