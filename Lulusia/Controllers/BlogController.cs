using Lulusia.Helpers;
using Lulusia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lulusia.Controllers
{
    public class BlogController : Controller
    {
        private readonly LayoutHelper _layoutHelper;
        private readonly BlogService _blogService;
        private readonly TopicService _topicService;
        public BlogController(LayoutHelper layoutHelper, BlogService blogService, TopicService topicService)
        {
            _layoutHelper = layoutHelper;
            _blogService = blogService;
            _topicService = topicService;
        }
        public async Task<IActionResult> Index(int topicId = -1)
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            var data = await _blogService.getByTopicId(topicId);
            var topics = await _topicService.GetAllActive();
            ViewBag.Topics = new SelectList(topics, "Id", "Name");
            ViewBag.LastestBlogs = await _blogService.GetLatestBlogs();

            return View(data);
        }
        public async Task<IActionResult> Details(int id)
        {
            string languageCode = Global.GetLanguageCode(Request);
            ViewBag.Layout = _layoutHelper.GetLayout(languageCode);
            var data = await _blogService.GetById(id);
            return View(data);
        }
    }
}
