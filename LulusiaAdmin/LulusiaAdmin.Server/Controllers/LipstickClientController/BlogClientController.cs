using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogClientController : ControllerBase
    {
        private readonly IBlogClientHelper _blogHelper;
        public BlogClientController(IBlogClientHelper blogHelper)
        {
            _blogHelper = blogHelper;
        }
        [HttpGet("getAllActive")]
        public IActionResult GetAllActive()
        {
            var data = _blogHelper.GetAllActive(ELanguages.VN.ToString());
            return Ok(data);
        }
        [HttpGet("getLatestBlogs")]
        public IActionResult GetLatestBlogs()
        {
            var data = _blogHelper.GetLatestBlogs(ELanguages.VN.ToString());
            return Ok(data);
        }
        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _blogHelper.GetById(id, ELanguages.VN.ToString());
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet("getByTopicId/{topicId}")]
        public IActionResult GetByTopicId(int topicId)
        {
            var data = _blogHelper.GetByTopicId(topicId, ELanguages.VN.ToString());
            return Ok(data);
        }
    }
}
