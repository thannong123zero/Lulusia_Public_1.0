//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FeedbackStatusController : ControllerBase
//    {
//        private readonly IFeedbackStatusHelper _feedbackStatusHelper;
//        private readonly IActionloggingService _actionLog;
//        public FeedbackStatusController(IFeedbackStatusHelper feedbackStatusHelper,
//            IActionloggingService actionLog)
//        {
//            _feedbackStatusHelper = feedbackStatusHelper;
//            _actionLog = actionLog;
//        }
//        [HttpGet("GetAll")]
//        [AllowAnonymous]
//        //[AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackStatus, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAll()
//        {
//            var data = await _feedbackStatusHelper.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackStatus, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _feedbackStatusHelper.GetByIdAsync(id);
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.ViewDetails.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(data)
//            };
//            await _actionLog.CreateAsync(userAction, token);
//            return Ok(data);
//        }

//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackStatus, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> Update([FromBody] FeedbackStatusViewModel model)
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
//                await _actionLog.CreateAsync(userAction, token);
//                return BadRequest(ModelState);
//            }
//            await _actionLog.CreateAsync(userAction, token);
//            await _feedbackStatusHelper.UpdateAsync(model);
//            return Ok();
//        }
//    }
//}
