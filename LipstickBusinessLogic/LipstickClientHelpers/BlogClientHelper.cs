using Common;
using Common.ViewModels.LipstickClientViewModels;
using LipstickBusinessLogic.ILipstickClientHelpers;
using LipstickDataAccess;

namespace LipstickBusinessLogic.LipstickClientHelpers
{
    public class BlogClientHelper : IBlogClientHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ServerAppConfig _appConfig;
        public BlogClientHelper(IUnitOfWork unitOfWork, ServerAppConfig appConfig)
        {
            _unitOfWork = unitOfWork;
            _appConfig = appConfig;
        }
        public IEnumerable<BlogClientViewModel> GetAllActive(string language)
        {
            var data = _unitOfWork.BlogRepository.GetAll(x => x.IsActive && !x.IsDeleted).Select(x => new BlogClientViewModel
            {
                Id = x.Id,
                TopicId = x.TopicId,
                Subject = language == ELanguages.VN.ToString() ? x.SubjectVN : x.SubjectEN,
                Description = language == ELanguages.VN.ToString() ? x.DescriptionVN : x.DescriptionEN,
                Content = language == ELanguages.VN.ToString() ? x.ContentVN : x.ContentEN,
                AvatarUrl = _appConfig.ServerUrl + EFolderNames.Blogs + "/" + x.Avatar
            });
            return data;
        }
        public BlogClientViewModel? GetById(int id, string language)
        {
            var data = _unitOfWork.BlogRepository.GetById(id);
            if (data == null)
            {
                return null;
            }
            return new BlogClientViewModel
            {
                Id = data.Id,
                TopicId = data.TopicId,
                Subject = language == ELanguages.VN.ToString() ? data.SubjectVN : data.SubjectEN,
                Description = language == ELanguages.VN.ToString() ? data.DescriptionVN : data.DescriptionEN,
                Content = language == ELanguages.VN.ToString() ? data.ContentVN : data.ContentEN,
                AvatarUrl = _appConfig.ServerUrl + EFolderNames.Blogs + "/" + data.Avatar
            };
        }

        public IEnumerable<BlogClientViewModel> GetByTopicId(int topicId, string language)
        {
            var data = _unitOfWork.BlogRepository.GetAll(x => x.IsActive && !x.IsDeleted && (topicId == -1 ? true : x.TopicId == topicId)).Select(x => new BlogClientViewModel
            {
                Id = x.Id,
                TopicId = x.TopicId,
                Subject = language == ELanguages.VN.ToString() ? x.SubjectVN : x.SubjectEN,
                Description = language == ELanguages.VN.ToString() ? x.DescriptionVN : x.DescriptionEN,
                Content = language == ELanguages.VN.ToString() ? x.ContentVN : x.ContentEN,
                AvatarUrl = _appConfig.ServerUrl + EFolderNames.Blogs + "/" + x.Avatar
            });
            return data;
        }

        public IEnumerable<BlogClientViewModel> GetLatestBlogs(string language)
        {
            var data = _unitOfWork.BlogRepository.GetAll(x => x.IsActive && !x.IsDeleted).Select(x => new BlogClientViewModel
            {
                Id = x.Id,
                TopicId = x.TopicId,
                Subject = language == ELanguages.VN.ToString() ? x.SubjectVN : x.SubjectEN,
                Description = language == ELanguages.VN.ToString() ? x.DescriptionVN : x.DescriptionEN,
                Content = language == ELanguages.VN.ToString() ? x.ContentVN : x.ContentEN,
                AvatarUrl = _appConfig.ServerUrl + EFolderNames.Blogs + "/" + x.Avatar
            });
            return data;
        }
    }
}
