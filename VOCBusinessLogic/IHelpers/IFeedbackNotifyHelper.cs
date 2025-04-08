namespace VOCBusinessLogic.IHelpers
{
    public interface IFeedbackNotifyHelper
    {
        public Task<int> GetNumberOfNewFeedbackOverdueAsync();
        //public Task<int> GetNumberOfForwardingFeedbackUnreadAsync();
        public Task<int> GetNumberOfForwardingFeedbackOverdueAsync();
        public Task NotifyEmail();
    }
}
