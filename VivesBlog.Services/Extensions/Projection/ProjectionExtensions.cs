using VivesBlog.DTO.Results;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions.Projection
{
    public static class ProjectionExtensions
    {
        public static IQueryable<ArticleResult> Project(this IQueryable<Article> query)
        {
            return query.Select(a => new ArticleResult
            {
                Id = a.Id,
                Title = a.Title,
                AuthorId = a.AuthorId,
                PublishedDate = a.PublishedDate,
                Description = a.Description,
                Content = a.Content,
                AuthorName = a.Author.LastName
            });
        }

        public static IQueryable<PersonResult> Project(this IQueryable<Person> query)
        {
            return query.Select(p => new PersonResult
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email
            });
        }
    }
}
