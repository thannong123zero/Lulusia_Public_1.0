using Common;
using Common.ViewModels.LipstickClientViewModels;
using Lulusia.Services;

namespace Lulusia.Helpers
{
    public sealed class HomePageHelper
    {
        private readonly TopicService _topicService;
        private readonly BannerService _bannerService;
        public HomePageHelper(TopicService topicService, BannerService bannerService)
        {
            _topicService = topicService;
            _bannerService = bannerService;
        }
        public async Task<HomePageViewModel> GetHomePageAsync()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            var banners = await _bannerService.GetAllActive();
            homePageViewModel.Topics = await _topicService.GetTopicsInHomePage();
            homePageViewModel.MainBanners = banners?.Where(x => x.BannerTypeId == (int)EBanners.MainBanner).ToList();
            homePageViewModel.SubBanners = banners?.Where(x => x.BannerTypeId == (int)EBanners.SubBanner).ToList();


            return homePageViewModel;
        }
    }
}
