namespace SendMail.Services
{
    public interface IMailServices
    {
        Task SendMailAsync(string MailTo, string subject, string body, IList<IFormFile> attachments = null);
    }
}
