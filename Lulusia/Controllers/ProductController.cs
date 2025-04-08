using Lulusia.Helpers;
using Lulusia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class ProductController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        private readonly CategoryService _categoryService;
        public ProductController(LayoutHelper layoutHelper, CategoryService categoryService)
        {
            _layoutHelper = layoutHelper;
            _categoryService = categoryService;
        }
        public IActionResult Index(int categoryId = -1, int subCategoryId = -1)
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            ViewBag.Categories = _categoryService.GetMenu().Result;
            return View();
        }
    }
}
