//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FeedbackPriorityController : ControllerBase
//    {
//        private readonly IFeedbackPriorityHelper _feedbackPriorityHelper;
//        private readonly IActionloggingService _actionLog;
//        public FeedbackPriorityController(IFeedbackPriorityHelper feedbackPriorityHelper,
//            IActionloggingService actionLog)
//        {
//            _feedbackPriorityHelper = feedbackPriorityHelper;
//            _actionLog = actionLog;
//        }
//        [HttpGet("GetAll")]
//        [AllowAnonymous]
//        //[AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackPriority, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAll()
//        {
//            var data = await _feedbackPriorityHelper.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackPriority, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _feedbackPriorityHelper.GetByIdAsync(id);
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.ViewDetails.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = JsonConvert.SerializeObject(data)
//            };
//            _actionLog.CreateAsync(userAction, token);
//            return Ok(data);
//        }

//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackPriority, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> Update([FromBody] FeedbackPriorityViewModel model)
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
//            _actionLog.CreateAsync(userAction, token);
//            await _feedbackPriorityHelper.UpdateAsync(model);
//            return Ok();
//        }

//    }
//}
