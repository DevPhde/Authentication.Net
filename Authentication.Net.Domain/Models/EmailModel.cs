using static System.Net.WebRequestMethods;

namespace Authentication.Net.Domain.Models
{
	public enum EmailTypeEnum
	{
		Welcome,
		RecoveryPassword
	}
	public class EmailModel
	{
		public string Subject { get; private set; }
		public string Content { get; private set; }
		public string Recipient { get; private set; }

		public EmailModel(string recipient, EmailTypeEnum type, string name, string welcomeLink = null, string newPassword = null)
		{
			Recipient = recipient;
			welcomeLink = "https://localhost:7011/confirmation/user/" + welcomeLink;
			switch (type)
			{
				case EmailTypeEnum.Welcome:
					Subject = "Welcome to Our Community!";
					Content = $"Hello {name},<br/><br/>" +
						  "Welcome to our service! We are excited that you've joined us. <br/>" +
						  $"To confirm your account, please click on the following link:<br/><br/>" +
						  $"<a href='{welcomeLink}'>Confirm Account</a>";
					break;
				case EmailTypeEnum.RecoveryPassword:
					Subject = "Password Recovery";
					Content = $"Hello {name},\n\nYou have requested a password recovery. Your new password is: {newPassword}\n\nPlease make sure to securely store this password and consider changing it after logging in.";
					break;
			}

		}
	}
}
