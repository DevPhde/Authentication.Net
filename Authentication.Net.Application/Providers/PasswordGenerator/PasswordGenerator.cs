namespace Authentication.Net.Application.Providers.PasswordGenerator
{
	public static class PasswordGenerator
	{
		public static string Generate()
		{
			string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

			Random random = new Random();

			string password = new string(Enumerable.Repeat(characters, 10)
				.Select(s=> s[random.Next(s.Length)])
				.ToArray());

			return password;
		}
	}
}
