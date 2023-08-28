using MediatR;

namespace Authentication.Net.Domain.Events
{
	public class UserCreatedWelcomeMailEvent : INotification
	{
		public string Name { get; private set;}
		public string Email { get; private set;}
		public UserCreatedWelcomeMailEvent(string name, string email)
		{
			Name = name;
			Email = email;
		}
    }
}
