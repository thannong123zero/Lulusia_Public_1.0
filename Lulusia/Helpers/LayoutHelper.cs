using Common.ViewModels.LipstickClientViewModels;
using Lulusia.Services;

namespace Lulusia.Helpers
{
    public sealed class LayoutHelper
    {
        private readonly CategoryService _navigationBarService;
        public LayoutHelper(CategoryService navigationBarService)
        {
            _navigationBarService = navigationBarService;
        }
        public LayoutViewModel GetLayout(string language)
        {
            LayoutViewModel model = new LayoutViewModel();
            model.HostName = Global.GetWebContentValueByKey(EWebContentKey.HostName, language);
            model.FacebookLink = Global.GetWebContentValueByKey(EWebContentKey.FacebookLink, language);
            model.InstagramLink = Global.GetWebContentValueByKey(EWebContentKey.InstagramLink, language);
            model.GithubLink = Global.GetWebContentValueByKey(EWebContentKey.GithubLink, language);
            model.YoutubeLink = Global.GetWebContentValueByKey(EWebContentKey.YoutubeLink, language);
            model.PhoneNumber = Global.GetWebContentValueByKey(EWebContentKey.PhoneNumber, language);
            model.Email = Global.GetWebContentValueByKey(EWebContentKey.Email, language);
            model.Address = Global.GetWebContentValueByKey(EWebContentKey.Address, language);
            model.CompanyName = Global.GetWebContentValueByKey(EWebContentKey.CompanyName, language);
            model.Categories = _navigationBarService.GetNavigationBar().Result;
            return model;
        }
    }
}
