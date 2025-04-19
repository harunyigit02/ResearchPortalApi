namespace FinalProjectWebApi.Entities.Abstract
{
    public class ArticleUploadDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public int PublishedBy { get; set; }
        public DateTime PublishedAt { get; set; }
        public int TotalViews { get; set; }
    }
}
