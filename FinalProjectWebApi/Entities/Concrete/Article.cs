using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Article
    {
        public int Id { get; set; }

        
        public int CategoryId { get; set; }     
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public int TotalViews { get; set; }
        public Category Category { get; set; }

    }
}
