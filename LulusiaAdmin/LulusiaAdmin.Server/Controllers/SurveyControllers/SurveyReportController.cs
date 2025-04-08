//using Microsoft.AspNetCore.Mvc;

//namespace LulusiaAdmin.Server.Controllers.SurveyControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SurveyReportController : ControllerBase
//    {
//        private readonly ISurveyReportHelper _surveyReportHelper;
//        private readonly ISurveyFormHelper _surveyFormHelper;
//        public SurveyReportController(
//            ISurveyReportHelper surveyReportHelper,
//            ISurveyFormHelper surveyFormHelper)
//        {
//            _surveyReportHelper = surveyReportHelper;
//            _surveyFormHelper = surveyFormHelper;
//        }

//        [HttpGet("GetBarChart")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.SurveyReport, ERoleClaim.View, EModule.Survey)]
//        public async Task<IActionResult> GetBarChart(int surveyId, DateTime startTime, DateTime finishTime)
//        {
//            var result = _surveyReportHelper.GetNumberOfSurverys(surveyId, startTime, finishTime);
//            return Ok(result);
//        }

//        [HttpGet("GetAverageScoreByDate")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
//        public async Task<IActionResult> GetAverageScoreByDate(int surveyId, DateTime startTime, DateTime finishTime)
//        {
//            var result = await _surveyReportHelper.GetAverageScoreByDate(surveyId, startTime, finishTime);
//            return Ok(result);
//        }

//        [HttpGet("GetClusteredBarChart")]
//        [AuthorizeEnumPolicy(ERoleClaimGroup.QuestionGroup, ERoleClaim.View, EModule.Survey)]
//        public async Task<IActionResult> GetClusteredBarChart(int surveyId, DateTime startTime, DateTime finishTime)
//        {
//            var result = await _surveyReportHelper.CompareAverageScoreByMonth(surveyId, startTime, finishTime);
//            return Ok(result);
//        }
//    }
//}
