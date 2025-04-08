//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VOCUISettingController : ControllerBase
//    {
//        private readonly IVOCUISettingHelper _vOCUISettingHelper;
//        private readonly IActionloggingService _actionLog;
//        public VOCUISettingController(IVOCUISettingHelper vOCUISettingHelper,
//            IActionloggingService actionlogging)
//        {
//            _vOCUISettingHelper = vOCUISettingHelper;
//            _actionLog = actionlogging;
//        }
//        [HttpGet("GetAll")]
//        [AllowAnonymous]
//        public IActionResult GetAll()
//        {
//            var data = _vOCUISettingHelper.GetAll();
//            return Ok(data);
//        }
//        [HttpGet("GetAll/{typeId}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.VOCUISetting, ERoleClaim.View, EModule.VOC)]
//        public IActionResult GetAll(int typeId)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = _vOCUISettingHelper.GetAll(typeId);
//            return Ok(data);
//        }
//        [HttpGet("GetByKey")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.VOCUISetting, ERoleClaim.View, EModule.VOC)]
//        public IActionResult GetByKey(int vocTypeId, string key)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.ViewDetails.ToString(),
//                Status = EUserActionStatus.Successful.ToString()
//            };
//            var data = _vOCUISettingHelper.GetByKey(vocTypeId, key);
//            userAction.Details = JsonConvert.SerializeObject(data);
//            _actionLog.CreateAsync(userAction, token);
//            return Ok(data);
//        }
//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.VOCUISetting, ERoleClaim.Update, EModule.VOC)]
//        public IActionResult Update([FromBody] ContentUIViewModel model, [FromHeader] int vocTypeId)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Update.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(model)
//            };

//            if (!ModelState.IsValid)
//            {
//                userAction.Status = EUserActionStatus.Failed.ToString();
//                _actionLog.CreateAsync(userAction, token);
//                return BadRequest(ModelState);
//            }
//            _vOCUISettingHelper.Update(model, vocTypeId);
//            _actionLog.CreateAsync(userAction, token);
//            return Ok();
//        }

//    }
//}
