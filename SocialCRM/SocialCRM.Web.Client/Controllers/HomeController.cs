using System.Web.Mvc;

namespace SocialCRM.Web.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}