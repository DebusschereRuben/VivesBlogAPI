using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesBlog.DTO.Results
{
    public class ArticleResult
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
    }
}
