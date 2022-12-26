using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace Shoper.BusinessLogic.Utility
{
    public interface IMailSender
    {
        public Task<bool> MailSend(Email mail);
    }
   
    public class MailSettings
    {
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class Email{

        public string Subject  { get; set; }
        public string  Body { get; set; }
        public InternetAddress  To { get; set; }
    }
    public class MailHelper : IMailSender
    {
       
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;
        public MailHelper(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            
        }

        public async Task<bool> MailSend(Email mail)
        {
            
				string host = _mailSettings.Host;
				int port = _mailSettings.Port;
				string fromAddress = _mailSettings.From;
				string userName = _mailSettings.From;
				string password = _mailSettings.Password;
                
                var message = new MimeMessage();
                message.Subject = mail.Subject;
			    message.From.Add(InternetAddress.Parse(fromAddress));
                message.To.Add(mail.To);
                message.Body= new TextPart(MimeKit.Text.TextFormat.Text) { Text = mail.Body };
				

				using (var client = new SmtpClient())
				{
					try
					{   // 465 port kullanın 
						client.Connect(host, port,MailKit.Security.SecureSocketOptions.SslOnConnect);
						client.AuthenticationMechanisms.Remove("XOAUTH2");
						client.Authenticate(userName, password);
						await client.SendAsync(message);
						return true;
					}
					catch
					{
					//log an error message or throw an exception or both.
					return false;
					}
					finally
					{
						client.Disconnect(true);
						client.Dispose();
					}
				}

			}
    }
}
