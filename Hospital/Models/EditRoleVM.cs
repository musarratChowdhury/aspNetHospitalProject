

using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
	public class EditRoleVM
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Role Name is required")]
		public string RoleName { get; set; }
		public List<string> Users { get; set; } = new List<string>();
	}
}
