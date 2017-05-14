using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using SocialCRM.Web.Client.Services;

namespace SocialCRM.Web.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserService userService;

        public HomeController(UserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var age = identity.Claims.Where(c => c.Type == "Name").Select(c => c.Value).SingleOrDefault();

            return View();
        }
    }
}