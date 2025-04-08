using BusinessLogic.IHelpers.IFeatureHelper;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.FeatureControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailHelper _emailHelper;
        public SendEmailController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }
        [HttpPost]
        [Route("sendSingleEmail")]
        public async Task<IActionResult> SendSingleEmail([FromForm] SingleMailData model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            bool result = await _emailHelper.SendSingleEmail(model);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        [Route("sendEmail")]
        public async Task<IActionResult> SendEmail(MailData model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            bool result = await _emailHelper.SendMailAsync(model);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        [Route("sendSingleEmailWithAttachment")]
        public async Task<IActionResult> SendSingleEmailWithAttachments([FromForm] MailDataWithAttachments model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            bool result = await _emailHelper.SendMailWithAttachments(model);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
