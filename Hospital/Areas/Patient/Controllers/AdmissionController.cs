using Autofac;
using Hospital.Areas.Patient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Patient.Controllers
{
	[Area("Patient")]
	[Authorize(Roles ="admin")]
	public class AdmissionController : Controller
	{
		

		private readonly ILifetimeScope _scope;
		private readonly ILogger<AdmissionController> _logger;

		public AdmissionController( ILogger<AdmissionController> logger, ILifetimeScope scope )
		{
			_scope = scope;
			_logger = logger;
		}
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		
		public IActionResult Create()
		{
			PatientCreateModel model = _scope.Resolve<PatientCreateModel>();
			return View(model);
		}

		[HttpPost,ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PatientCreateModel model)
		{
			if(ModelState.IsValid)
			{
				model.ResolveDependency(_scope);
				await model.CreatePatient();
			}
			return View();
		}
	}
}
