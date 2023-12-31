﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Net.Application.DTOs
{
	public class UserLoginDTO
	{
		[Required(ErrorMessage = "The Email is Required.")]
		[MinLength(3)]
		[MaxLength(255)]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "The Password is Required.")]
		[MinLength(3)]
		[MaxLength(255)]
		[DisplayName("Password")]
		public string Password { get; set; }
	}
}
