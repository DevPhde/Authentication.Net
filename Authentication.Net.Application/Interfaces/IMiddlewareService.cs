namespace Authentication.Net.Application.Interfaces
{
	public interface IMiddlewareService
	{
		bool VerifyJWT(string jwtKey);
	}
}
