using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class StartPageController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View("AdminHomePage");
            if (User.IsInRole("Publisher"))
                return View("PublisherHomePage");
            if (User.IsInRole("Subscriber"))
                return View("SubscriberHomePage");
            return View("DefaultHomePage");
        }
    }
}