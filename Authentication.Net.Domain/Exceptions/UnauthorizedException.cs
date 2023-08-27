namespace Authentication.Net.Domain.Exceptions
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException(string error) : base(error) { }
	}
}
