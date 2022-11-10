using Autofac;
using Hospital.Areas.Inventory.Models;


using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Inventory.Controllers
{
	[Area("Inventory")]
	public class InventoryController : Controller
	{
		

		private readonly ILifetimeScope _scope;
		private readonly ILogger<InventoryController> _logger;

		public InventoryController( ILogger<InventoryController> logger, ILifetimeScope scope )
		{
			_scope = scope;
			_logger = logger;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			InventoryCreateModel model = _scope.Resolve<InventoryCreateModel>();
			return View(model);
		}

		[HttpPost,ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(InventoryCreateModel model)
		{
			if(ModelState.IsValid)
			{
				model.ResolveDependency(_scope);
				await model.CreateInventory();
			}
			return View();
		}
	}
}
