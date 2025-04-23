using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EducationPlatform.helpers
{
	public class EmailSender: IEmailSender
	{
		private readonly MailSettings emailSender;
		public EmailSender(IOptions<MailSettings> options)
		{
			emailSender = options.Value;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			MailMessage message = new()
			{
				From = new MailAddress(emailSender.Email!, emailSender.DisplayName),
				Body = htmlMessage,
				Subject = subject,
				IsBodyHtml = true
			};

			SmtpClient smtpClient = new(emailSender.Host)
			{
				Port = emailSender.Port,
				Credentials = new NetworkCredential(emailSender.Email, emailSender.password),
				EnableSsl = true
			};

			message.To.Add(email);
			await smtpClient.SendMailAsync(message);
			smtpClient.Dispose();
		}

	}
}
