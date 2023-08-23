using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Application.DTOs
{
	public class UserDTO
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Cpf { get; set; }
		public string Password { get; set; }
		public bool IsEmailConfirmed { get; set; }
		public bool IsActive { get; set; }
		public bool IsAdmin { get; set; }
		public string Auth { get; set; }
	}
}
