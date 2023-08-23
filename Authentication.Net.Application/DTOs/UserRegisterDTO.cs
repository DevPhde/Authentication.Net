using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Net.Application.DTOs
{
	public class UserRegisterDTO
	{
		[Required(ErrorMessage = "The Name is Required.")]
		[MinLength(3)]
		[MaxLength(255)]
		[DisplayName("FullName")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "The Email is Required.")]
		[MinLength(3)]
		[MaxLength(255)]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "The CPF is Required.")]
		[StringLength(14)]
		[DisplayName("CPF")]
		public string Cpf { get; set; }

		[Required(ErrorMessage = "The Password is Required.")]
		[MinLength(3)]
		[MaxLength(100)]
		[DisplayName("Password")]
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
	}
}
