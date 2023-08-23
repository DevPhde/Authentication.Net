namespace Authentication.Net.Domain.Exceptions
{
	public class InternalErrorException : Exception
	{
        public InternalErrorException(string error) : base(error)
        { }
    }
}
