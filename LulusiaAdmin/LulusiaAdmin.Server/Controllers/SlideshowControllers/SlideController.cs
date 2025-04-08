//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LulusiaAdmin.Server.Controllers.SlideshowControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class SlideController : ControllerBase
//    {
//        private readonly ISlideHelper _slideHelper;
//        public SlideController(ISlideHelper slideHelper)
//        {
//            _slideHelper = slideHelper;
//        }
//        [HttpGet("GetAll/{slideThemeId?}")]
//        public async Task<IActionResult> GetAll(int slideThemeId = -1)
//        {
//            var data = await _slideHelper.GetSlidesByThemeIdAsync(slideThemeId);
//            return Ok(data);
//        }
//        [HttpGet("GetSlideshow")]
//        public async Task<IActionResult> GetSlideshow(int slideThemeId)
//        {
//            var data = await _slideHelper.GetSlidesByThemeIdAsync(slideThemeId);
//            data.Where(s => s.IsActive);
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _slideHelper.GetByIdAsync(id);
//            return Ok(data);
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create([FromForm] SlideViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(model);
//            }
//            await _slideHelper.CreateAsync(model);
//            return Ok();
//        }

//        [HttpPut("Update")]
//        public async Task<IActionResult> Update([FromForm] SlideViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(model);
//            }
//            await _slideHelper.UpdateAsync(model);

//            return Ok();
//        }

//        [HttpPatch("SoftDelete/{id}")]
//        public async Task<IActionResult> SoftDelete(int id)
//        {
//            await _slideHelper.SoftDeleteAsync(id);
//            return Ok();
//        }

//        [HttpPatch("Restore/{id}")]
//        public async Task<IActionResult> Restore(int id)
//        {
//            await _slideHelper.RestoreAsync(id);
//            return Ok();
//        }

//        [HttpDelete("Delete")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _slideHelper.DeleteAsync(id);
//            return Ok();
//        }

//    }
//}
