using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryClientController : ControllerBase
    {
        private readonly ICategoryClientHelper _categoryHelper;

        public CategoryClientController(ICategoryClientHelper categoryHelper)
        {
            _categoryHelper = categoryHelper;
        }
        [HttpGet("getNavigationBar")]
        public IActionResult GetNavigationBar()
        {
            var data = _categoryHelper.GetNavigationBar(ELanguages.VN.ToString());
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }
        [HttpGet("getMenu")]
        public IActionResult GetMenu()
        {
            var data = _categoryHelper.GetMenu(ELanguages.VN.ToString());
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }
    }
}
