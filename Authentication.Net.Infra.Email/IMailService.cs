using Authentication.Net.Domain.Models;

namespace Authentication.Net.Infra.Email
{
	public interface IMailService
	{
		Task SendMail(EmailModel emailModel);
	}
}
