using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Application.Interfaces
{
	public interface IJwtProvider
	{
		bool IsTokenValid(string token);
		string GenerateJwt(string userEmail);
		string DecodeJwt(string jwt);
	}
}
