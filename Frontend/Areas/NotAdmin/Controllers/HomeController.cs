using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {         
            return View();
        }
    }
}