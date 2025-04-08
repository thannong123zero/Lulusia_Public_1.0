using Common;
using Common.ViewModels.LipstickClientViewModels;
using LipstickBusinessLogic.ILipstickClientHelpers;
using LipstickDataAccess;

namespace LipstickBusinessLogic.LipstickClientHelpers
{
    public class TopicClientHelper : ITopicClientHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ServerAppConfig _appConfig;
        public TopicClientHelper(IUnitOfWork unitOfWork, ServerAppConfig appConfig)
        {
            _unitOfWork = unitOfWork;
            _appConfig = appConfig;
        }

        public IEnumerable<TopicClientViewModel> GetAllActive(string language)
        {
            var data = _unitOfWork.TopicRepository.GetAll(x => x.IsActive && !x.IsDeleted).Select(x => new TopicClientViewModel
            {
                Id = x.Id,
                Name = language == ELanguages.VN.ToString() ? x.NameVN : x.NameEN,
                AvatarUrl = _appConfig.ServerUrl + x.Avatar
            });
            return data;
        }

        public IEnumerable<TopicClientViewModel> GetTopicsInHomePage(string language)
        {
            List<TopicClientViewModel> result = new List<TopicClientViewModel>();
            var data = _unitOfWork.TopicRepository.GetAll(s => s.IsActive && !s.IsDeleted && s.InHomePage, orderBy: p => p.OrderBy(s => s.Priority));
            foreach (var item in data)
            {
                TopicClientViewModel topic = new TopicClientViewModel();
                topic.Id = item.Id;
                topic.Name = string.Equals(language, ELanguages.EN.ToString()) ? item.NameEN : item.NameVN;
                topic.AvatarUrl = _appConfig.ServerUrl + item.Avatar;
                result.Add(topic);
            }

            return result;
        }
    }
}
