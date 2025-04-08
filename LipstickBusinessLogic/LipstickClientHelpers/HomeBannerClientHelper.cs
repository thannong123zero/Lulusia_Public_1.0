using Common;
using Common.ViewModels.LipstickClientViewModels;
using LipstickBusinessLogic.ILipstickClientHelpers;
using LipstickDataAccess;

namespace LipstickBusinessLogic.LipstickClientHelpers
{
    public class HomeBannerClientHelper : IHomeBannerClientHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ServerAppConfig _appConfig;
        public HomeBannerClientHelper(IUnitOfWork unitOfWork, ServerAppConfig appConfig)
        {
            _unitOfWork = unitOfWork;
            _appConfig = appConfig;
        }

        public IEnumerable<HomeBannerClientViewModel> GetAllActive(string language)
        {
            var data = _unitOfWork.HomeBannerRepository.GetAll(filter: s => !s.IsDeleted && s.IsActive, orderBy: p => p.OrderBy(s => s.Priority));
            return data.Select(x => new HomeBannerClientViewModel
            {
                Id = x.Id,
                BannerTypeId = x.BannerTypeId,
                Subject = language == ELanguages.VN.ToString() ? x.SubjectVN : x.SubjectEN,
                Description = language == ELanguages.VN.ToString() ? x.DescriptionVN : x.DescriptionEN,
                ImageUrl = string.Concat(_appConfig.ServerUrl, x.ImageName).Replace(@"\", @"/"),
                RedirectUrl = x.RedirectUrl,
                Tags = new List<string>() { "Tag 1", "Tag 2", "Tag 3" }
            });

        }
    }
}
