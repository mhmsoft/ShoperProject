using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Shoper.BusinessLogic.Utility
{
    public interface IMailSender
    {
        public Task MailSend(Email mail);
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
        public string  To { get; set; }
    }
    public class MailHelper : IMailSender
    {
       
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;
        public MailHelper(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            
        }

        public async Task MailSend(Email mail)
        {
            string host = _mailSettings.Host;
            int port = _mailSettings.Port;
            string fromAddress = _mailSettings.From;
            string userName = _mailSettings.From;
            string password = _mailSettings.Password;
            using (MailMessage mm = new MailMessage(fromAddress, mail.To))
            {
                mm.Subject = mail.Subject;
                mm.Body = mail.Body;

                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    await smtp.SendMailAsync(mm);
                   
                }
            }
        }
    }
}
