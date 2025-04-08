using Lulusia.Helpers;
using Lulusia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandService _brandService;
        private readonly LayoutHelper _layoutHelper;
        public BrandController(BrandService brandService, LayoutHelper layoutHelper)
        {
            _brandService = brandService;
            _layoutHelper = layoutHelper;
        }
        public async Task<IActionResult> Index()
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            var data = await _brandService.GetAllActive();
            return View(data);
        }
    }
}
