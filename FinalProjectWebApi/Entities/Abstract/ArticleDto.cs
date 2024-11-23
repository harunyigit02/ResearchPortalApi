namespace FinalProjectWebApi.Entities.Abstract
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int PublishedBy { get; set; }
        public DateTime PublishedAt { get; set; }
        public int TotalViews { get; set; }

    }
}
