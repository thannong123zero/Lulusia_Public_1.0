//using Microsoft.AspNetCore.Mvc;

//namespace LulusiaAdmin.Server.Controllers.VOCControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VOCReportController : ControllerBase
//    {
//        private readonly IVOCReportHelper _vocReportHelper;
//        public VOCReportController(IVOCReportHelper vocReportHelper)
//        {
//            _vocReportHelper = vocReportHelper;
//        }
//        [HttpGet("GetBarChart")]
//        public async Task<IActionResult> GetBarChart(DateTime startTime, DateTime finishTime, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            BarChartViewModel barChartViewModel = await _vocReportHelper.GetNumberOfTicketsAsync(startTime, finishTime, applyTo, mallId, officeId);
//            return Ok(barChartViewModel);
//        }
//        [HttpGet("GetStackHorizontalBarChart")]
//        public async Task<IActionResult> GetStackHorizontalBarChart(DateTime startTime, DateTime finishTime, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            StackBarViewModel stackBarViewModel = await _vocReportHelper.CompareNumberOfFeedbackTypesByMonthAsync(startTime, finishTime, applyTo, mallId, officeId);

//            return Ok(stackBarViewModel);
//        }
//        [HttpGet("GetStackVerticalBarChart")]
//        public async Task<IActionResult> GetStackVerticalBarChart(DateTime startTime, DateTime finishTime, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            StackBarViewModel stackBarViewModel = await _vocReportHelper.CompareNumberOfTicketsByMonthAsync(startTime, finishTime, applyTo, mallId, officeId);

//            return Ok(stackBarViewModel);
//        }
//        [HttpGet("GetTreeMap")]
//        public async Task<IActionResult> GetTreeMap(DateTime time, int applyTo = 0, int mallId = -1, int officeId = -1)
//        {
//            TreeMapViewModel treeMap = await _vocReportHelper.GetNumberOfFeedbackTypesByMonthAsync(time, applyTo, mallId, officeId);

//            return Ok(treeMap);
//        }
//    }
//}
