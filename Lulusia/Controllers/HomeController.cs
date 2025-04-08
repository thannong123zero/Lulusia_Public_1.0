using Lulusia.Helpers;
using Lulusia.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lulusia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LayoutHelper _layoutHelper;
        private readonly HomePageHelper _homePageHelper;
        public HomeController(ILogger<HomeController> logger, LayoutHelper layoutHelper, HomePageHelper homePageHelper)
        {
            _logger = logger;
            _layoutHelper = layoutHelper;
            _homePageHelper = homePageHelper;
        }

        public async Task<IActionResult> Index()
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            var data = await _homePageHelper.GetHomePageAsync();
            return View(data);
        }
        [HttpGet]
        public IActionResult SetLanguage()
        {
            string languageCode = Global.GetLanguageCode(Request);
            string vn = ELanguage.VN.ToString();
            if (string.Equals(languageCode, vn))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
            else
            {
                Response.Cookies.Append(
                   CookieRequestCultureProvider.DefaultCookieName,
                   CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("vi-VN")),
                   new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
               );
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Lost()
        {
            return View();
        }
    }
}