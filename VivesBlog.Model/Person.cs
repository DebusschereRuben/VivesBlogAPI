using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Model
{
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }

        public IList<Article> Articles { get; set; } = new List<Article>();
    }
}
