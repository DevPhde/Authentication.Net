using Authentication.Net.Application.DTOs;

namespace Authentication.Net.Application.Interfaces
{
	public interface IUserServices
	{
		Task<string> AuthorizeUser(UserLoginDTO userDTO);
		Task CreateNewUser(UserRegisterDTO userDTO);
		Task DeleteUser(int id);
		Task DisableUser(int id);
		Task EnableUser(int id);
		Task<UserDTO> GetById(int id);
		Task<IEnumerable<UserDTO>> GetUsers();
		Task RecoveryPassword(string email);
		Task ConffirmAccount(string jwtKey);
	}
}
