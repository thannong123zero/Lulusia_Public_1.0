//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FeedbackController : ControllerBase
//    {
//        private readonly IFeedbackHelper _feedbackHelper;
//        private readonly IVOCReportHelper _vocReportHelper;
//        private readonly IActionloggingService _actionLog;
//        private readonly IJwtService _jwtService;
//        public FeedbackController(
//            IFeedbackHelper feedbackHelper,
//            IActionloggingService actionLog,
//            IVOCReportHelper vocReportHelper,
//            IJwtService jwtService)
//        {
//            _feedbackHelper = feedbackHelper;
//            _actionLog = actionLog;
//            _vocReportHelper = vocReportHelper;
//            _jwtService = jwtService;
//        }
//        [HttpGet("GetAll")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetAll(int? applyTo, int? mallId, int? officeId, string? phoneNumber, DateTime startDate, DateTime endDate, int feedbackTypeID = -1, int statusID = -1, int priorityId = -1, int pageIndex = 1)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, mallId, officeId);

//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = await _feedbackHelper.GetAllAsync(paramModel.ApplyTo, paramModel.MallId, paramModel.OfficeId, feedbackTypeID, statusID, priorityId, startDate, endDate, pageIndex, phoneNumber);
//            return Ok(data);
//        }

//        [HttpGet("GetFeedbackReport")]
//        //[AllowAnonymous]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetFeedbackReport(int? applyTo, int? mallId, int? officeId, string? phoneNumber, DateTime startDate, DateTime endDate, int feedbackTypeID = -1, int statusID = -1, int priorityId = -1, int pageIndex = 1)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, mallId, officeId);
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.View.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            _actionLog.CreateAsync(userAction, token);
//            var data = await _feedbackHelper.GetReportAsync(paramModel.ApplyTo, paramModel.MallId, paramModel.OfficeId, feedbackTypeID, statusID, priorityId, startDate, endDate, phoneNumber);
//            return Ok(data);
//        }

//        [HttpGet("GetById/{id}")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.View, EModule.VOC)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var data = await _feedbackHelper.GetByIdAsync(id);
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
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.Create, EModule.VOC)]
//        public async Task<IActionResult> Create([FromForm] FeebackClientViewModel model)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Create.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//            };
//            if (!ModelState.IsValid)
//            {
//                userAction.Status = EUserActionStatus.Failed.ToString();
//                _actionLog.CreateAsync(userAction, token);
//                return BadRequest(ModelState);
//            }
//            _actionLog.CreateAsync(userAction, token);
//            await _feedbackHelper.CreateAsync(model, token);
//            return Ok();
//        }
//        /// <summary>
//        /// Web voc client use this method
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("CreateClientFeedback")]
//        [AllowAnonymous]
//        public async Task<IActionResult> CreateClientFeeback([FromForm] FeebackClientViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            await _feedbackHelper.CreateAsync(model);
//            return Ok();
//        }

//        [HttpPut("Update")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> Update([FromBody] FeedbackViewModel model)
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
//            await _feedbackHelper.UpdateAsync(model, token);
//            return Ok();
//        }

//        [HttpGet("ExportExcel")]
//        //[AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.Export, EModule.VOC)]
//        public async Task<IActionResult> ExportExcel(DateTime? startTime, DateTime? finishTime, int? feedbackTypeId, int? statusId, int? priorityId, int? applyTo, int? mallId, int? officeId)
//        {
//            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, mallId, officeId);
//            string path = await _vocReportHelper.ExportFeedback(startTime, finishTime, feedbackTypeId, statusId, priorityId, paramModel.ApplyTo, paramModel.MallId, paramModel.OfficeId);

//            UserActionModel userAction = new UserActionModel()
//            {
//                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
//                ActionName = EUserAction.Export.ToString(),
//                Status = EUserActionStatus.Successful.ToString(),
//                Details = ""
//            };
//            if (string.IsNullOrEmpty(path))
//            {
//                userAction.Status = EUserActionStatus.Failed.ToString();
//                _actionLog.CreateAsync(userAction, token);
//                return BadRequest();
//            }

//            _actionLog.CreateAsync(userAction, token);
//            var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
//            var fileName = Path.GetFileName(path);
//            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
//        }

//        [HttpPost("ForwardFeedback")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.Feedback, ERoleClaim.Update, EModule.VOC)]
//        public async Task<IActionResult> ForwardFeedback(ForwardFeedbackViewModel model)
//        {
//            bool result = await _feedbackHelper.ForwardFeedbackAsync(model);
//            if (!result)
//            {
//                return BadRequest();
//            }
//            return Ok();
//        }
//        /// <summary>
//        /// This method use for departement
//        /// </summary>
//        /// <param name="code"></param>
//        /// <returns></returns>
//        [HttpGet("GetForwardedFeedbackByCode")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetForwardedFeedbackByCode(string code, int forwardFeedbackId, string language)
//        {
//            FeedbackViewModel model = await _feedbackHelper.GetByCodeAsync(code, forwardFeedbackId, language);
//            if (model == null)
//            {
//                return BadRequest();
//            }
//            return Ok(model);
//        }
//        [HttpPut("HandleForwardedFeedback")]
//        [AllowAnonymous]
//        public async Task<IActionResult> HandleForwardedFeedback(ForwardFeedbackViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(model);
//            }
//            await _feedbackHelper.ResponseFeedbackByDeparmentAsync(model);
//            return RedirectToAction("ThankYourSupport");
//        }
//    }
//}
