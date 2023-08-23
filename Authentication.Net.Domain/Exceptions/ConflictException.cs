namespace Authentication.Net.Domain.Exceptions
{
	public class ConflictException : Exception
	{
		public ConflictException(string error) : base(error) { }
	}
}
