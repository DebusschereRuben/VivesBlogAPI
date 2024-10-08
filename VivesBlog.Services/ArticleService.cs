using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.DTO.Requests;
using VivesBlog.DTO.Results;
using VivesBlog.Model;
using VivesBlog.Services.Extensions.Projection;
using VivesBlog.Services.Extensions.Validation;

namespace VivesBlog.Services
{
    public class ArticleService
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticleService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<ArticleResult>> Find()
        {
            return await _dbContext.Articles
                .Include(a => a.Author)
                .Project()
                .ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

            var article = await _dbContext.Articles
                .Project()
                .FirstOrDefaultAsync(a => a.Id == id);

            serviceResult.Data = article;
            if (article is null)
            {
                serviceResult.NotFound(nameof(Article), id);
            }

            return serviceResult;
        }

        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var serviceResult = new ServiceResult<ArticleResult>();

            serviceResult.Validate(request);

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var article = new Article
            {
                Title = request.Title,
                PublishedDate = request.PublishedDate,
                Description = request.Description,
                Content = request.Content,
                AuthorId = request.AuthorId,
            };

            //article.PublishedDate = DateTime.UtcNow;

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var serviceResult = new ServiceResult<ArticleResult>();
 
            var article = await _dbContext.Articles
                .FirstOrDefaultAsync(p => p.Id == id);

            if (article is null)
            {
                serviceResult.NotFound(nameof(Article), id);
                return serviceResult;
            }

            article.Title = article.Title;
            article.Description = article.Description;
            article.Content = article.Content;
            article.AuthorId = article.AuthorId;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }
        //TODO continue
        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var serviceResult = new ServiceResult();

            var article = await _dbContext.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article is null)
            {
                serviceResult.NotFound(nameof(Article), id);
                return serviceResult;
            }

            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();

            serviceResult.Deleted(nameof(Article));
            return serviceResult;
        }

    }
}
