using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.DTO.Requests;
using VivesBlog.DTO.Results;

namespace VivesBlog.Services.Extensions.Validation
{
    public static class ArticleValidator
    {
        public static void Validate(this ServiceResult<ArticleResult> serviceResult, ArticleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                serviceResult.NotEmpty(nameof(request.Title));
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                serviceResult.NotEmpty(nameof(request.Description));
            }

            if (string.IsNullOrWhiteSpace(request.Content))
            {
                serviceResult.NotEmpty(nameof(request.Content));
            }
        }
    }
}
