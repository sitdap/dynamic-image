using System.Web.Mvc;

namespace SoundInTheory.DynamicImage.Website.Controllers
{
	public class GettingStartedController : Controller
	{
		public ActionResult Index()
		{
			return RedirectToAction("Installation");
		}
		
		public ActionResult Installation()
		{
			return View();
		}
	}
}