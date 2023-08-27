using Authentication.Net.Domain.Models;
using System.Net;
using System.Net.Mail;

namespace Authentication.Net.Infra.Email
{
	public class MailService : IMailService
	{
		public SmtpClient SmtpClient { get; set; } = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
		{
			Credentials = new NetworkCredential("2715abc435b944", "9ccde0897b63d1"),
			EnableSsl = true,
		};
		public async Task SendMail(EmailModel emailModel)
		{
			await Task.Run(() =>
			{
				MailMessage mailMessage = new()
				{
					From = new MailAddress("noreply.teste@teste.com"),
					To = { emailModel.Recipient },
					Subject = emailModel.Subject,
					Body = emailModel.Content,
					IsBodyHtml = true
				};

				try
				{
					SmtpClient.Send(mailMessage);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			});
		}

	}
}
