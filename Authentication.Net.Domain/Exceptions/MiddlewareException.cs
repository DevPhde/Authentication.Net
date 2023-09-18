namespace Authentication.Net.Domain.Exceptions
{
	public class MiddlewareException : Exception
	{
		public MiddlewareException(string error = "") : base(error) { }
	}
}
