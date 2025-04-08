using Common;
using Common.Models;
using Common.Services.ActionLoggingServices;
using Common.Services.JwtServices;
using Common.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyBusinessLogic.IHelpers;

namespace LulusiaAdmin.Server.Controllers.SurveyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyFormController : ControllerBase
    {
        private readonly ISurveyFormHelper _surveyFormHelper;
        private readonly IJwtService _jwtService;
        private readonly IActionloggingService _actionLog;
        public SurveyFormController(ISurveyFormHelper surveyFormHelper, IJwtService jwtService, IActionloggingService actionLog)
        {
            _surveyFormHelper = surveyFormHelper;
            _jwtService = jwtService;
            _actionLog = actionLog;
        }

        [HttpGet("GetAll")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll(int? applyTo, int? mallId, int? officeId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //SystemRequestViewModel paramModel = _jwtService.GetSystemRequestViewModel(token, applyTo, mallId, officeId);
            //var data = await _surveyFormHelper.GetAllAsync(paramModel.ApplyTo, paramModel.MallId, paramModel.OfficeId);
            return Ok();
        }

        [HttpGet("GetAllByStoreId/{storeId}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAllByStoreId(int storeId)
        {
            var data = await _surveyFormHelper.GetAllAsync(storeId);
            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _surveyFormHelper.GetByIdAsync(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(data);
        }

        [HttpGet("GetEagerById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetEagerById(int id)
        {
            var data = _surveyFormHelper.GetEagerSurveyFormByID(id); string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(data);
        }

        [HttpGet("GetEagerUIById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEagerUIById(int id, string language)
        {
            var data = await _surveyFormHelper.GetEagerSurveyUIByID(id, language);
            return Ok(data);
        }

        [HttpPost("Create")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.Create, EModule.Survey)]
        public async Task<IActionResult> Create([FromBody] SurveyFormViewModel model)
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
            await _surveyFormHelper.CreateAsync(model);
            return Ok();
        }

        [HttpPut("Update")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.Update, EModule.Survey)]
        public async Task<IActionResult> Update([FromBody] SurveyFormViewModel model)
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
            await _surveyFormHelper.UpdateAsync(model);
            return Ok();
        }

        [HttpPatch("SoftDelete/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyForm, ERoleClaim.SoftDelete, EModule.Survey)]
        public async Task<IActionResult> Delete(int id)
        {
            await _surveyFormHelper.SoftDeleteAsync(id);
            return Ok();
        }
    }
}
