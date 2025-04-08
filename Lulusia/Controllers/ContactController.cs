using Common.ViewModels.LipstickClientViewModels;
using Lulusia.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class ContactController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        public ContactController(LayoutHelper layoutHelper)
        {
            _layoutHelper = layoutHelper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FeedbackClientViewModel model)
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            if (ModelState.IsValid)
            {
                // Send email
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
