using Authentication.Net.Application.DTOs;
using Authentication.Net.Application.Interfaces;
using Authentication.Net.Application.Provider.Bcrypt;
using Authentication.Net.Domain.Entities;
using Authentication.Net.Domain.Exceptions;
using Authentication.Net.Domain.Interfaces;
using Authentication.Net.Domain.Validation;
using AutoMapper;

namespace Authentication.Net.Application.UseCases
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IBcryptProvider _cryptProvider;

		public UserServices(IUserRepository userRepository, IMapper mapper, IBcryptProvider cryptProvider)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_cryptProvider = cryptProvider;
		}
		public Task<string> AuthorizeUser(UserLoginDTO userDTO)
		{
			throw new NotImplementedException();
		}

		public async Task CreateNewUser(UserRegisterDTO userDTO)
		{
			try
			{
				User user = new(userDTO.FullName, userDTO.Email, userDTO.Cpf, userDTO.Password, userDTO.IsAdmin);

				bool cpfIsAlreadyExists = await _userRepository.GetByCpf(userDTO.Cpf) != null;
				bool emailIsAlreadyExists = await _userRepository.GetByEmail(userDTO.Email) != null;

				if (cpfIsAlreadyExists || emailIsAlreadyExists)
				{
					throw new ConflictException($"{(cpfIsAlreadyExists ? "CPF" : "")}{(cpfIsAlreadyExists && emailIsAlreadyExists ? " and " : "")}{(emailIsAlreadyExists ? "Email" : "")} already Registered.");
				}
				user.UserHashPassword(_cryptProvider.HashPassword(user.Password));
				await _userRepository.Create(user);

			}
			catch (DomainExceptionValidation ex)
			{
				throw new BadRequestException(ex.Message);
			}
			catch (InternalErrorException ex)
			{
				throw new InternalErrorException(ex.Message);
			}
		}

		public Task DeleteUser(int id)
		{
			throw new NotImplementedException();
		}

		public Task DisableUser(int id)
		{
			throw new NotImplementedException();
		}

		public Task EnableUser(int id)
		{
			throw new NotImplementedException();
		}

		public Task<UserDTO> GetById(int? id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<UserRegisterDTO>> GetUsers()
		{
			throw new NotImplementedException();
		}

		public Task<string> RecoveryPassword(string email)
		{
			throw new NotImplementedException();
		}
	}
}
