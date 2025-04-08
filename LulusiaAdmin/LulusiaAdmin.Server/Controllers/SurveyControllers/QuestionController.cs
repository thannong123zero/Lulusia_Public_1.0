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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionHelper _questionHelper;
        private readonly IJwtService _jwtService;
        private readonly IActionloggingService _actionLog;
        public QuestionController(IQuestionHelper questionHelper, IJwtService jwtService, IActionloggingService actionLog)
        {
            _questionHelper = questionHelper;
            _jwtService = jwtService;
            _actionLog = actionLog;
        }
        [HttpGet("GetAll")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var data = await _questionHelper.GetAllAsync();
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.View, EUserActionStatus.Successful);

            return Ok(data);
        }

        [HttpGet("GetAllByQuestionGroupId/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll(int id)
        {
            var data = await _questionHelper.GetAllAsync(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.ViewDetails, EUserActionStatus.Successful);

            return Ok(data);
        }

        [HttpGet("GetAllByFilter")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll(int? applyTo, int? questionGroupId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, null, null);
            var data = await _questionHelper.GetAllAsync(1, questionGroupId);
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.View, EUserActionStatus.Successful);

            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _questionHelper.GetByIdAsync(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.ViewDetails, EUserActionStatus.Successful);

            return Ok(data);
        }

        [HttpPost("Create")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.Create, EModule.Survey)]
        public async Task<IActionResult> Create([FromBody] QuestionViewModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!ModelState.IsValid)
            {
                _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.Create, EUserActionStatus.Failed);

                return BadRequest(ModelState);
            }
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.Create, EUserActionStatus.Successful);
            await _questionHelper.CreateAsync(model);
            return Ok();
        }

        [HttpPut("Update")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.Update, EModule.Survey)]
        public async Task<IActionResult> Update([FromBody] QuestionViewModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _questionHelper.UpdateAsync(model);
            return Ok();
        }

        [HttpPatch("SoftDelete/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.Question, ERoleClaim.SoftDelete, EModule.Survey)]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionHelper.SoftDeleteAsync(id);
            return Ok();
        }
    }
}
