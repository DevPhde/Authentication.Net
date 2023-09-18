namespace Authentication.Net.Domain.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string error) : base(error)
		{ }
	}
}
