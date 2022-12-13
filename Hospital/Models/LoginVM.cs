using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hospital.Models
{
	public class LoginVM
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

		public IList<AuthenticationScheme>? ExternalLogins { get; set; }

		public string? ReturnUrl { get; set; }
	}
}
