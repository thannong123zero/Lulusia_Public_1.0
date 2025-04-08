using Common;
using Common.Models;
using Common.Services.ActionLoggingServices;
using Microsoft.AspNetCore.Mvc;
using SurveyBusinessLogic.IHelpers;

namespace LulusiaAdmin.Server.Controllers.SurveyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeHelper _questionTypeHelper;
        private readonly IActionloggingService _actionLog;
        public QuestionTypeController(IQuestionTypeHelper questionTypeHelper, IActionloggingService actionLog)
        {
            _questionTypeHelper = questionTypeHelper;
            _actionLog = actionLog;
        }
        [HttpGet("GetAll")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionType, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _questionTypeHelper.GetAllAsync();
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.View, EUserActionStatus.Successful);

            return Ok(data);
        }
    }
}
