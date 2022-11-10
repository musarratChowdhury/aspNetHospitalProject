using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Inventory.Controllers
{
	public class HomeController : Controller
	{
		[Area("Inventory")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
