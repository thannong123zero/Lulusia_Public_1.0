using Common;
using Common.Models;
using Common.Services.ActionLoggingServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyBusinessLogic.IHelpers;

namespace LulusiaAdmin.Server.Controllers.SurveyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantHelper _participantHelper;
        private readonly IActionloggingService _actionLog;
        public ParticipantController(IParticipantHelper participant, IActionloggingService actionLog)
        {
            _participantHelper = participant;
            _actionLog = actionLog;
        }
        [HttpGet("GetAll")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyResult, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetAll(DateTime? startDate, DateTime? endDate, int surveyFormId = -1, int pageIndex = 1)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var data = await _participantHelper.GetAllAsync(startDate, endDate, surveyFormId, pageIndex);
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.View,EUserActionStatus.Successful);
            return Ok(data);
        }
        //[HttpPost("Create")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Create([FromBody] SurveyUIViewModel model)
        //{
        //    await _participantHelper.CreateAsync(model);
        //    return Ok();
        //}
        [HttpGet("GetEagerById/{id}")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyResult, ERoleClaim.View, EModule.Survey)]
        public async Task<IActionResult> GetEagerById(int id)
        {
            var data = _participantHelper.GetEagerCustomerSurveyByID(id);
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.ViewDetails, EUserActionStatus.Successful);

            return Ok(data);
        }
        [HttpGet("ExportExcel")]
        //[AuthorizeEnumPolicy(ERoleClaimGroup.SurveyResult, ERoleClaim.Export, EModule.Survey)]
        public async Task<IActionResult> ExportExcel(DateTime? startTime, DateTime? finishTime, int surveyFormId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string path = await _participantHelper.ExportExcel(startTime, finishTime, surveyFormId);
            if (string.IsNullOrEmpty(path))
            {
                _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.Export, EUserActionStatus.Failed);
                return BadRequest();
            }
            _actionLog.CreateAsync(token, ControllerContext.ActionDescriptor.ControllerName, EUserAction.Export, EUserActionStatus.Successful);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
            var fileName = Path.GetFileName(path);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
