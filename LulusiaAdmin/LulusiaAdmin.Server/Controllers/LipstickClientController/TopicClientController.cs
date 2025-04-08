using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicClientController : ControllerBase
    {
        private readonly ITopicClientHelper _topicClientHelper;
        public TopicClientController(ITopicClientHelper topicClientHelper)
        {
            _topicClientHelper = topicClientHelper;
        }
        [HttpGet("getTopicsInHomePage")]
        public IActionResult GetTopicsInHomePage()
        {
            var data = _topicClientHelper.GetTopicsInHomePage(ELanguages.VN.ToString());
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }
        [HttpGet("getAllActive")]
        public IActionResult GetAllActive()
        {
            var data = _topicClientHelper.GetAllActive(ELanguages.VN.ToString());
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }
    }
}
