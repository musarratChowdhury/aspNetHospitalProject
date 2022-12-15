using Microsoft.Build.Framework;

namespace Hospital.Models
{
	public class CreateRoleVM
	{
		[Required]
		public string RoleName { get; set; }
	}
}
