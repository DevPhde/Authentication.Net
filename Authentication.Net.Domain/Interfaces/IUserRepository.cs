using Authentication.Net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task Create(User user);
		Task Update(User user);
		Task<User?> GetById(int id);
		Task<User?> GetByCpf(string cpf);
		Task<User?> GetByEmail(string email);
		Task<IEnumerable<User>> GetUsers();
		Task Remove(User user);
	}
}
