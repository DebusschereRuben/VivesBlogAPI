using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesBlog.SDK;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleSDK _articleSDK;

        public HomeController(ArticleSDK articleSDK)
        {
            _articleSDK = articleSDK;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleSDK.Find();
            return View(articles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
