using Common.ViewModels.LipstickClientViewModels;
namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface ITopicClientHelper
    {
        public IEnumerable<TopicClientViewModel> GetTopicsInHomePage(string language);
        public IEnumerable<TopicClientViewModel> GetAllActive(string language);
    }
}
