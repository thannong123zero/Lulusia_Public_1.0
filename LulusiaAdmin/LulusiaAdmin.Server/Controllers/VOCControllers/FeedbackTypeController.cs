//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FeedbackTypeController : ControllerBase
//    {
//        private readonly IFeedbackTypeHelper _feedbackTypeHelper;
//        private readonly IActionloggingService _actionLog;
//        public FeedbackTypeController(IFeedbackTypeHelper feedbackTypeHelper,
//            IActionloggingService actionLog)
//        {
//            _feedbackTypeHelper = feedbackTypeHelper;
//            _actionLog = actionLog;
//        }
//        [HttpGet("GetAll/{typeId}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAll(int typeId = -1)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = await _feedbackTypeHelper.GetAllAsync(typeId);
//            return Ok(data);
//        }

//        [HttpGet("GetAll/{language}/{typeId}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll(string language, int typeId)
//        {
//            var data = await _feedbackTypeHelper.GetAllAsync(language, typeId);
//            return Ok(data);
//        }

//        [HttpGet("GetAllActive/{typeId}")]
//        [AllowAnonymous]
//        //[AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAllActive(int typeId)
//        {
//            //string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            //UserActionModel userAction = new UserActionModel()
//            //{
//            //    ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//            //    ActionName = EUserAction.View.ToString(),
//            //    Status = EUserActionStatus.Successful.ToString(),
//            //};
//            //_actionLog.CreateAsync(userAction, token);
//            var data = await _feedbackTypeHelper.GetAllActiveAsync(typeId);
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _feedbackTypeHelper.GetByIdAsync(id);
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

//        [HttpPost("Create")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.Create, EModule.VOC)]
//        public async Task<IActionResult> Create([FromBody] FeedbackTypeViewModel model)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Create.ToString(),
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
//            await _feedbackTypeHelper.CreateAsync(model);
//            return Ok();
//        }

//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> Update([FromBody] FeedbackTypeViewModel model)
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
//            await _feedbackTypeHelper.UpdateAsync(model);
//            return Ok();
//        }

//        [HttpPatch("SoftDelete/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.SoftDelete, EModule.VOC)]
//        public async Task<IActionResult> SoftDelete(int id)
//        {
//            await _feedbackTypeHelper.SoftDeleteAsync(id);
//            return Ok();
//        }

//        [HttpPatch("Restore/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.Restore, EModule.VOC)]
//        public async Task<IActionResult> Restore(int id)
//        {
//            await _feedbackTypeHelper.RestoreAsync(id);
//            return Ok();
//        }

//        [HttpDelete("Delete/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.FeedbackType, ERoleClaim.Delete, EModule.VOC)]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _feedbackTypeHelper.DeleteAsync(id);
//            return Ok();
//        }
//    }
//}
