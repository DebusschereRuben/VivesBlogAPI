using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.DTO.Requests;
using VivesBlog.DTO.Results;
using VivesBlog.SDK;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleSDK _articleSDK;
        private readonly PersonSDK _personSDK;

        public ArticlesController(
            ArticleSDK articleSDK, PersonSDK personSDK)
        {
            _articleSDK = articleSDK;
            _personSDK = personSDK;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = _articleSDK.Find();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                var article = new ArticleResult
                {
                    Title = request.Title,
                    Content = request.Content,
                    Description = request.Description,
                    AuthorId = request.AuthorId,
                    PublishedDate = request.PublishedDate,
                };
                return View(article);
            }

            await _articleSDK.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _articleSDK.Get(id);

            if (!result.IsSuccess || result.Data is null)
            {
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                var result = await _articleSDK.Get(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return RedirectToAction("Index");
                }
                var article = result.Data;
                article.Title = request.Title;
                article.Description = request.Description;
                article.Content = request.Content;

                return View(article);
            }

            await _articleSDK.Update(id, request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _articleSDK.Get(id);
            if (!result.IsSuccess || result.Data is null)
            {
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleSDK.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
