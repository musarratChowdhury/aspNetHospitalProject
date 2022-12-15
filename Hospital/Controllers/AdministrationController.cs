using Hospital.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
	public class AdministrationController : Controller
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public AdministrationController(RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleVM roleVM)
		{
			if(ModelState.IsValid)
			{
				var newRole = new ApplicationRole();
				newRole.Name = roleVM.RoleName;

				 var result = await _roleManager.CreateAsync(newRole);

				if(result.Succeeded)
				{
					return RedirectToAction("ListRole", "Administration");
				}
				foreach(var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(roleVM);
		}

		[HttpGet]
		public IActionResult ListRole()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}

		[HttpGet]
		public async Task<IActionResult> EditRole(string id)
		{
			// Find the role by Role ID
			var role = await _roleManager.FindByIdAsync(id);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}
			else
			{
				var model = new EditRoleVM
				{
					Id = role.Id,
					RoleName = role.Name
				};

				// Retrieve all the Users
				foreach (var user in _userManager.Users)
				{
					// If the user is in this role, add the username to
					// Users property of EditRoleViewModel. This model
					// object is then passed to the view for display
					if (await _userManager.IsInRoleAsync(user, role.Name))
					{
						model.Users.Add(user.UserName);
					}
				}
				return View(model);

			}
			
		}
		// This action responds to HttpPost and receives EditRoleViewModel
		[HttpPost]
		public async Task<IActionResult> EditRole(EditRoleVM model)
		{
			var role = await _roleManager.FindByIdAsync(model.Id.ToString());

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
				return View("NotFound");
			}
			else
			{
				role.Name = model.RoleName;

				// Update the Role using UpdateAsync
				var result = await _roleManager.UpdateAsync(role);

				if (result.Succeeded)
				{
					return RedirectToAction("ListRole");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				return View(model);
			}
		}
		[HttpGet]
		public async Task<IActionResult> EditUsersInRole(string roleId)
		{
			ViewBag.roleId = roleId;

			var role = await _roleManager.FindByIdAsync(roleId);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			var model = new List<UserRoleViewModel>();

			foreach (var user in _userManager.Users)
			{
				var userRoleViewModel = new UserRoleViewModel
				{
					UserId = user.Id.ToString(),
					UserName = user.UserName
				};

				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					userRoleViewModel.IsSelected = true;
				}
				else
				{
					userRoleViewModel.IsSelected = false;
				}

				model.Add(userRoleViewModel);
			}

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			for (int i = 0; i < model.Count; i++)
			{
				var user = await _userManager.FindByIdAsync(model[i].UserId);

				IdentityResult result = null;

				if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
				{
					result = await _userManager.AddToRoleAsync(user, role.Name);
				}
				else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
				{
					result = await _userManager.RemoveFromRoleAsync(user, role.Name);
				}
				else
				{
					continue;
				}

				if (result.Succeeded)
				{
					if (i < (model.Count - 1))
						continue;
					else
						return RedirectToAction("EditRole", new { Id = roleId });
				}
			}

			return RedirectToAction("EditRole", new { Id = roleId });
		}

	}
}
