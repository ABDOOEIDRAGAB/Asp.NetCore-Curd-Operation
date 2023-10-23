using System.ComponentModel.DataAnnotations;

namespace SendMail.Ddos
{
    public class RequestMail
    {
        [Required]
        public string ToMail {  get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}
