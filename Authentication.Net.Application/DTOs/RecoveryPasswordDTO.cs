using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Net.Application.DTOs
{
	public class RecoveryPasswordDTO
	{
		[Required(ErrorMessage = "The Email is Required.")]
		[MinLength(3)]
		[MaxLength(255)]
		[DisplayName("Email")]
		public string Email { get; set; }
	}
}
