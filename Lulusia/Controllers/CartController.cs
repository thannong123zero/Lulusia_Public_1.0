using Lulusia.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class CartController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        public CartController(LayoutHelper layoutHelper)
        {
            _layoutHelper = layoutHelper;
        }
        public IActionResult Index()
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            return View();
        }
    }
}
