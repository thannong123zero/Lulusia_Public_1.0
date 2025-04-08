using Common;
using Lulusia.Helpers;
using Lulusia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        private readonly InformationPageService _informationPageService;
        private readonly LayoutHelper _layoutHelper;
        public PrivacyPolicyController(InformationPageService informationPageService, LayoutHelper layoutHelper)
        {
            _informationPageService = informationPageService;
            _layoutHelper = layoutHelper;
        }
        public async Task<IActionResult> Index()
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            var data = await _informationPageService.GetInforPageByPageTypeId((int)EPageTypes.PrivacyPolicy);
            return View(data);
        }
    }
}
