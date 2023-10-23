using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SendMail.Services
{
    public class MailServices : IMailServices
    {
        private readonly MailSetting _setting;

        public MailServices(IOptions<MailSetting> setting)
        {
            _setting = setting.Value;
        }

        public async Task SendMailAsync(string MailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Subject = subject,
                Sender = MailboxAddress.Parse(_setting.Email)
            };

            email.To.Add(MailboxAddress.Parse(MailTo));

            var builder = new BodyBuilder();

            if(attachments != null)
            {
                byte[] filesbyte;
                foreach (var file in attachments)
                {
                    if (file.Length>0)
                    {
                        using var datastream = new MemoryStream();
                        file.CopyTo(datastream);
                        filesbyte = datastream.ToArray();

                        builder.Attachments.Add(file.FileName, filesbyte, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            email.From.Add(new MailboxAddress(_setting.DisplayUser, _setting.Email));

            using var smtp = new SmtpClient();

            smtp.Connect(_setting.Host, _setting.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_setting.Email, _setting.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
