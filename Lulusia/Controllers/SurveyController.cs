using Microsoft.AspNetCore.Mvc;

namespace Lulusia.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
