using MailKit;
using Microsoft.AspNetCore.Mvc;
using SendMail.Ddos;
using SendMail.Services;

namespace SendMail.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MailingsController : ControllerBase
    {
        private readonly IMailServices _mailServices;

        public MailingsController(IMailServices mailService)
        {
            _mailServices = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm]RequestMail request)
        {
            await _mailServices.SendMailAsync(request.ToMail, request.Subject, request.Body, request.Attachments);
            return Ok();
        }
    }
}
