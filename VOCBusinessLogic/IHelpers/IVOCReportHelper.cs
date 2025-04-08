//namespace VOCBusinessLogic.IHelpers
//{
//    public interface IVOCReportHelper
//    {
//        public Task<VOCReportViewModel> VOCReportViewModel();
//        //public Task<FeedbackReportViewModel> GetFeedbackReportAsync(DateTime? startTime, DateTime? finishTime, int mallId = -1);
//        public Task<BarChartViewModel> GetNumberOfFeedbacksByMonthAsync(DateTime time, int mallId = -1);
//        public Task<BarChartViewModel> GetNumberOfTicketsAsync(DateTime firstTime, DateTime secondTime, int applyTo = 0, int mallId = -1, int officeId = -1);
//        public Task<StackBarViewModel> CompareNumberOfFeedbackTypesByMonthAsync(DateTime firstMonth, DateTime secondMonth, int applyTo = 0, int mallId = -1, int officeId = -1);
//        public Task<StackBarViewModel> CompareNumberOfTicketsByMonthAsync(DateTime firstMonth, DateTime secondMonth, int applyTo = 0, int mallId = -1, int officeId = -1);
//        public Task<TreeMapViewModel> GetNumberOfFeedbackTypesByMonthAsync(DateTime time, int applyTo = 0, int mallId = -1, int officeId = -1);
//        public Task<string> ExportFeedback(DateTime? startTime, DateTime? finishTime, int? feedbackTypeId, int? statusId, int? priorityId, int? applyTo, int? mallId, int? officeId);
//    }
//}
