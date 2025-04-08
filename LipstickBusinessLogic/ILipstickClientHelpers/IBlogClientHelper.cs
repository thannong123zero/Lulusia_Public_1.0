using Common.ViewModels.LipstickClientViewModels;

namespace LipstickBusinessLogic.ILipstickClientHelpers
{
    public interface IBlogClientHelper
    {
        public IEnumerable<BlogClientViewModel> GetAllActive(string language);
        public IEnumerable<BlogClientViewModel> GetLatestBlogs(string language);
        public BlogClientViewModel? GetById(int id, string language);
        public IEnumerable<BlogClientViewModel> GetByTopicId(int topicId, string language);

    }
}
