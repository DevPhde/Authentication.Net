using Authentication.Net.Application.DTOs;
using Authentication.Net.Application.Interfaces;
using Authentication.Net.Application.Providers.PasswordGenerator;
using Authentication.Net.Domain.Entities;
using Authentication.Net.Domain.Events;
using Authentication.Net.Domain.Exceptions;
using Authentication.Net.Domain.Interfaces;
using Authentication.Net.Domain.Validation;
using AutoMapper;
using MediatR;

namespace Authentication.Net.Application.UseCases
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IBcryptProvider _cryptProvider;
		private readonly IMediator _mediator;
		private readonly IJwtProvider _jwtProvider;
		public UserServices(IUserRepository userRepository, IMapper mapper, IBcryptProvider cryptProvider, IMediator mediator, IJwtProvider jwtProvider)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_cryptProvider = cryptProvider;
			_mediator = mediator;
			_jwtProvider = jwtProvider;
		}
		public async Task<string> AuthorizeUser(UserLoginDTO userDTO)
		{
			try
			{
				User user = await _userRepository.GetByEmail(userDTO.Email) ?? throw new UnauthorizedException("Email or Password incorrect.");
				if (!user.IsActive)
				{
					throw new UnauthorizedException("Account not activated, contact the administrator.");
				}

				if (!user.IsEmailConfirmed)
				{
					_ = _mediator.Publish(new UserCreatedWelcomeMailEvent(user.FullName, user.Email));
					throw new UnauthorizedException("Account not confirmed. A confirmation email has been sent to the registered email address.");
				}

				if (!_cryptProvider.IsIdenticalPassword(userDTO.Password, user.Password))
				{
					throw new UnauthorizedException("Email or Password incorrect.");
				}

				string token = _jwtProvider.GenerateJwt(user.Email);
				user.UpdateAuth(token);
				await _userRepository.Update(user);
				return token;
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}

		public async Task ConffirmAccount(string jwtKey)
		{
			try
			{
				if (_jwtProvider.IsTokenValid(jwtKey))
				{
					string userMail = _jwtProvider.DecodeJwt(jwtKey);

					User user = await _userRepository.GetByEmail(userMail);
					user.UserConffirmAccount();
					await _userRepository.Update(user);
				}
			}
			catch (InternalErrorException)
			{
				throw;
			}
			catch (UnauthorizedException)
			{
				throw;
			}
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

				_ = _mediator.Publish(new UserCreatedWelcomeMailEvent(user.FullName, user.Email));
			}
			catch (DomainExceptionValidation ex)
			{
				throw new BadRequestException(ex.Message);
			}
			catch (InternalErrorException)
			{
				throw;
			}

		}

		public async Task DeleteUser(int id)
		{
			try
			{
				User user = await _userRepository.GetById(id) ?? throw new BadRequestException("Invalid ID.");
				await _userRepository.Remove(user);
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}

		public async Task DisableUser(int id)
		{
			try
			{
				User user = await _userRepository.GetById(id) ?? throw new BadRequestException("Invalid ID.");
				user.IsUserEnabled(false);
				await _userRepository.Update(user);
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}

		public async Task EnableUser(int id)
		{
			try
			{
				User user = await _userRepository.GetById(id) ?? throw new BadRequestException("Invalid ID.");
				user.IsUserEnabled(true);
				await _userRepository.Update(user);
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}

		public async Task<UserDTO> GetById(int id)
		{
			User user = await _userRepository.GetById(id) ?? throw new BadRequestException("Invalid Id.");
			return _mapper.Map<UserDTO>(user);
		}

		public async Task<IEnumerable<UserDTO>> GetUsers()
		{
			try
			{
				var users = await _userRepository.GetUsers();
				return _mapper.Map<IEnumerable<UserDTO>>(users);
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}

		public async Task RecoveryPassword(string email)
		{
			try
			{
				User user = await _userRepository.GetByEmail(email) ?? throw new BadRequestException("Email not registered.");
				string newPassword = PasswordGenerator.Generate();
				user.updatePassword(_cryptProvider.HashPassword(newPassword));
				await _userRepository.Update(user);
				_ = _mediator.Publish(new UserRecoveryPasswordSendMailEvent(user.FullName, user.Email, newPassword));
			}
			catch (InternalErrorException)
			{
				throw;
			}
		}
	}
}
