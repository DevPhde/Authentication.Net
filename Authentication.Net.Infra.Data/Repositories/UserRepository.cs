using Authentication.Net.Domain.Entities;
using Authentication.Net.Domain.Exceptions;
using Authentication.Net.Domain.Interfaces;
using Authentication.Net.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Net.Infra.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _userContext;
		public UserRepository(ApplicationDbContext context)
		{
			_userContext = context;
		}

		public async Task Create(User user)
		{
			try
			{
				_userContext.Add(user);
				await _userContext.SaveChangesAsync();
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task<User?> GetByCpf(string cpf)
		{
			try
			{
				return await _userContext.Users.FindAsync(cpf);
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task<User?> GetByEmail(string email)
		{
			try
			{
				return await _userContext.Users.FindAsync(email);
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task<User?> GetById(int id)
		{
			try
			{
				return await _userContext.Users.FindAsync(id);
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task<IEnumerable<User>> GetUsers()
		{
			try
			{
				return await _userContext.Users.ToListAsync();
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task Remove(User user)
		{
			try
			{
				_userContext.Remove(user);
				await _userContext.SaveChangesAsync();
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}

		public async Task Update(User user)
		{
			try
			{
				_userContext.Update(user);
				await _userContext.SaveChangesAsync();
			}
			catch
			{
				throw new InternalErrorException("Internal Error, try again later.");
			}
		}
	}
}
