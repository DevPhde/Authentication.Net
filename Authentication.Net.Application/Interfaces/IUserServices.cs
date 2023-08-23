using Authentication.Net.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Application.Interfaces
{
	public interface IUserServices
	{
		Task<IEnumerable<UserRegisterDTO>> GetUsers();
		Task<UserDTO> GetById(int? id);
		Task CreateNewUser(UserRegisterDTO userDTO);
		Task DisableUser(int id);
		Task EnableUser(int id);
		Task DeleteUser(int id);
		Task<string> AuthorizeUser(UserLoginDTO userDTO);
		Task<string> RecoveryPassword(string email);
	}
}
