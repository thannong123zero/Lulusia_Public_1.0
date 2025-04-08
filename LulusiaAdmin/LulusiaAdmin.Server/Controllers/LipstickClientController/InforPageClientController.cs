using Common;
using LipstickBusinessLogic.ILipstickClientHelpers;
using Microsoft.AspNetCore.Mvc;

namespace LulusiaAdmin.Server.Controllers.LipstickClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class InforPageClientController : ControllerBase
    {
        private readonly IInforPageClientHelper _inforPageClientHelper;
        public InforPageClientController(IInforPageClientHelper inforPageClientHelper)
        {
            _inforPageClientHelper = inforPageClientHelper;
        }
        [HttpGet]
        [Route("getByPageTypeId/{pageTypeId}")]
        public IActionResult GetByPageTypeId(int pageTypeId)
        {
            var data = _inforPageClientHelper.GetFirstDataByPageTypeId(pageTypeId, ELanguages.VN.ToString());
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }
    }
}
