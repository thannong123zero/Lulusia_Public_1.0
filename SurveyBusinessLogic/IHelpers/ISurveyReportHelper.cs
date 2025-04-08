using Common.ViewModels.SurveyViewModels;

namespace SurveyBusinessLogic.IHelpers
{
    public interface ISurveyReportHelper
    {
        public Task<string> ExportSurveyResult(int surveyId);
        public Task<BarChartViewModel> GetAverageScoreByDate(int surveyId, DateTime startTime, DateTime finishTime);
        public Task<ClusteredBarChartViewModel> CompareAverageScoreByMonth(int surveyId, DateTime startTime, DateTime finishTime);
        public BarChartViewModel GetNumberOfSurverys(int surveyId, DateTime startTime, DateTime finishTime);
    }
}
