using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandClientController : ControllerBase
    {
        private readonly IBrandClientHelper _brandHelper;
        public BrandClientController(IBrandClientHelper brandHelper)
        {
            _brandHelper = brandHelper;
        }
        [HttpGet("getAllActive")]
        public IActionResult GetAllActive()
        {
            var data = _brandHelper.GetAllActive(ELanguages.EN.ToString());
            return Ok(data);
        }
    }
}
