using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.DTO.Requests;
using VivesBlog.DTO.Results;
using VivesBlog.SDK;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonSDK _personSDK;
        private readonly ArticleSDK _articleSDK;

        public PeopleController(PersonSDK personSDK, ArticleSDK articleSDK)
        {
            _personSDK = personSDK;
            _articleSDK = articleSDK;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personSDK.Find();

            return View(people);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var person = new PersonResult
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                };

                return await CreateEditView(person);
            }

            await _personSDK.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var result = await _personSDK.Get(id);

            if (result.IsSuccess ||result.Data is null)
            {
                return RedirectToAction("Index");
            }
            
            return await CreateEditView(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var result = await _personSDK.Get(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return RedirectToAction("Index");
                }

                var person = result.Data;
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Email = request.Email;

                return await CreateEditView(person);
            }

            await _personSDK.Update(id, request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var person = await _personSDK.Get(id);

            return View(person.Data);
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personSDK.Delete(id);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> CreateEditView(PersonResult? person = null)
        {
            var articles = await _articleSDK.Find();
            ViewBag.Articles = articles;
            return View(person);
        }
    }
}
