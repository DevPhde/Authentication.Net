using Authentication.Net.Domain.Events;
using Authentication.Net.Domain.Models;
using Authentication.Net.Infra.Email;
using MediatR;

namespace Authentication.Net.Application.Events
{
	public class UserRecoveryPasswordSendMailEventHandler : INotificationHandler<UserRecoveryPasswordSendMailEvent>
	{
		private readonly IMailService _mailService;

		public UserRecoveryPasswordSendMailEventHandler(IMailService mailService)
		{
			_mailService = mailService;
		}

		public Task Handle(UserRecoveryPasswordSendMailEvent notification, CancellationToken cancellationToken)
		{
			EmailModel emailModel = new(notification.Email, EmailTypeEnum.RecoveryPassword, notification.Name, null, notification.Password);
			_mailService.SendMail(emailModel);
			return Task.CompletedTask;
		}
	}
}
