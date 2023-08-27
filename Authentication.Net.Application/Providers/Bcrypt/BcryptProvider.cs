using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Net.Application.Interfaces;
using BC = BCrypt.Net;

namespace Authentication.Net.Application.Provider.Bcrypt
{
    public class BcryptProvider : IBcryptProvider
	{
		public string HashPassword(string password)
		{
			return BC.BCrypt.HashPassword(password);
		}

		public bool IsIdenticalPassword(string password, string hashedPassword)
		{
			return BC.BCrypt.Verify(password, hashedPassword);
		}
	}
}
