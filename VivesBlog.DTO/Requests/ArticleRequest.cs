using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesBlog.DTO.Requests
{
    public class ArticleRequest
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public int? AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Content { get; set; }
    }
}
