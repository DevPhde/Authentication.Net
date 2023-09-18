using Authentication.Net.Application.Interfaces;
using Authentication.Net.Domain.Exceptions;

namespace Authentication.Net.Application.Services
{
	public class MiddlewareServices
	{
        public static bool VerifyJWT(string jwtKey, IJwtProvider jwtProvider)
		{
			try
			{
				jwtProvider.IsTokenValid(jwtKey);
				return true;
			}
			catch (UnauthorizedException)
			{
				throw new MiddlewareException();
			}
		}
	}
}
