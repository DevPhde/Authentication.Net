using Authentication.Net.Application.Interfaces;
using Authentication.Net.Domain.Events;
using Authentication.Net.Domain.Models;
using Authentication.Net.Infra.Email;
using MediatR;

namespace Authentication.Net.Application.Events
{
	public class CreateUserEventHandler : INotificationHandler<UserCreatedEvent>
	{
		private readonly IMailService _mailService;
		private readonly IJwtProvider _jwtProvider;
        public CreateUserEventHandler(IMailService mailService, IJwtProvider jwtProvider)
        {
			_mailService = mailService;
			_jwtProvider = jwtProvider;
			
        }
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
		{
			string userIdentifier = _jwtProvider.GenerateJwt(notification.Email);
			EmailModel emailModel = new(notification.Email, EmailTypeEnum.Welcome, notification.Name, userIdentifier);
			_mailService.SendMail(emailModel);
			return Task.CompletedTask;
		}
	}
}
