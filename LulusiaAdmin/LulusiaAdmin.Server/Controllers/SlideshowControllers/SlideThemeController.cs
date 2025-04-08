//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LulusiaAdmin.Server.Controllers.SlideshowControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class SlideThemeController : ControllerBase
//    {
//        private readonly ISlideThemeHelper _topicSlideHelper;
//        public SlideThemeController(ISlideThemeHelper topicSlideHelper)
//        {
//            _topicSlideHelper = topicSlideHelper;
//        }

//        [HttpGet("GetAll")]
//        public async Task<IActionResult> GetAll()
//        {
//            var data = await _topicSlideHelper.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _topicSlideHelper.GetByIdAsync(id);
//            return Ok(data);
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create([FromBody] SlideThemeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            await _topicSlideHelper.CreateAsync(model);
//            return Ok();
//        }

//        [HttpPut("Update")]
//        public async Task<IActionResult> Update([FromBody] SlideThemeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            await _topicSlideHelper.UpdateAsync(model);
//            return Ok();
//        }

//        [HttpPatch("SoftDelete/{id}")]
//        public async Task<IActionResult> SoftDelete(int id)
//        {
//            await _topicSlideHelper.SoftDeleteAsync(id);
//            return Ok();
//        }
//        [HttpPatch("Restore/{id}")]
//        public async Task<IActionResult> Restore(int id)
//        {
//            await _topicSlideHelper.RestoreAsync(id);
//            return Ok();
//        }
//        [HttpDelete("Delete/{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _topicSlideHelper.DeleteAsync(id);
//            return Ok();
//        }
//    }
//}
