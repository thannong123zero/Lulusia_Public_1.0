using Common;
using Common.Models;
using Common.Services.ActionLoggingServices;
using Common.Services.JwtServices;
using Common.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyBusinessLogic.IHelpers;

namespace LulusiaAdmin.Server.Controllers.SurveyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionGroupController : ControllerBase
    {
        private readonly IQuestionGroupHelper _questionGroupHelper;
        private readonly IJwtService _jwtService;
        private readonly IActionloggingService _actionLog;
        public QuestionGroupController(IQuestionGroupHelper questionGroupHelper, IJwtService jwtService, IActionloggingService actionLog)
        {
            _questionGroupHelper = questionGroupHelper;
            _jwtService = jwtService;
            _actionLog = actionLog;
        }
        [HttpGet("GetAll")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll(bool getActive = false, int applyTo = -1)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, null, null);
            var data = await _questionGroupHelper.GetAllAsync(1, getActive);
            return Ok(data);
        }
        [HttpGet("GetAllActive")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAllActive()
        {
            var data = await _questionGroupHelper.GetAllByActiveAsync(true);
            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _questionGroupHelper.GetByIdAsync(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(data);
        }

        [HttpGet("GetEagerById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetEagerById(int id)
        {
            var data = _questionGroupHelper.GetEagerQuestionGroupByID(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(data);
        }

        [HttpGet("GetEagerAllElements")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
        public IActionResult GetEagerAllElements(bool getActive = true)
        {
            var data = _questionGroupHelper.GetEagerAllElements(getActive);
            return Ok(data);
        }

        [HttpPost("Create")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.Create, EModule.Survey)]
        public async Task<IActionResult> Create([FromBody] QuestionGroupViewModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            UserActionModel userAction = new UserActionModel()
            {
                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
                ActionName = EUserAction.Create.ToString(),
                Status = EUserActionStatus.Successful.ToString(),
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _questionGroupHelper.CreateAsync(model);
            return Ok();
        }

        [HttpPut("Update")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.Update, EModule.Survey)]
        public async Task<IActionResult> Update([FromBody] QuestionGroupViewModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            UserActionModel userAction = new UserActionModel()
            {
                ControllerName = ControllerContext.ActionDescriptor.ControllerName,
                ActionName = EUserAction.Update.ToString(),
                Status = EUserActionStatus.Successful.ToString(),
                Details = JsonConvert.SerializeObject(model)
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _questionGroupHelper.UpdateAsync(model);
            return Ok();
        }

        [HttpPatch("SoftDelete/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.SoftDelete, EModule.Survey)]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionGroupHelper.SoftDeleteAsync(id);
            return Ok();
        }
    }
}
