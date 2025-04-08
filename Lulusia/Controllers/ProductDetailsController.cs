using Lulusia.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        public ProductDetailsController(LayoutHelper layoutHelper)
        {
            _layoutHelper = layoutHelper;
        }
        public IActionResult Index(string ID)
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            return View();
        }
    }
}
