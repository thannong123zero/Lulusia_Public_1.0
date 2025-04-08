using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeBannerClientController : ControllerBase
    {
        private readonly IHomeBannerClientHelper _homeBannerClientHelper;
        public HomeBannerClientController(IHomeBannerClientHelper homeBannerClientHelper)
        {
            _homeBannerClientHelper = homeBannerClientHelper;
        }
        [HttpGet("getAllActive")]
        public IActionResult GetAllActive()
        {
            var result = _homeBannerClientHelper.GetAllActive(ELanguages.VN.ToString());
            return Ok(result);
        }
    }
}
