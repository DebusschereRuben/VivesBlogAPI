using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesBlog.DTO.Requests
{
    public class PersonRequest
    {
        [DisplayName("First name")]
        [Required]
        public required string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

 
        //public IList<Article> Articles { get; set; } = new List<Article>();
    }
}
