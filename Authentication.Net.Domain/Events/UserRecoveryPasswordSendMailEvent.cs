using MediatR;

namespace Authentication.Net.Domain.Events
{
	public class UserRecoveryPasswordSendMailEvent : INotification
	{
		public string Name { get; private set; }
		public string Email { get; private set; }
		public string Password { get; private set; }

		public UserRecoveryPasswordSendMailEvent(string name, string email, string password)
		{
			Name = name;
			Email = email;
			Password = password;
		}
	}
}
