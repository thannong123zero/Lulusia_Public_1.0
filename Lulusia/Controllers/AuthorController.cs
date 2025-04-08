using Lulusia.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        public AuthorController(LayoutHelper layoutHelper)
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
