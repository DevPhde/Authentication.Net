using MediatR;

namespace Authentication.Net.Domain.Events
{
	public class UserCreatedEvent : INotification
	{
		public string Name { get; private set;}
		public string Email { get; private set;}
		public UserCreatedEvent(string name, string email)
		{
			Name = name;
			Email = email;
		}
    }
}
