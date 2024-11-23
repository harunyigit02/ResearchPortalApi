namespace FinalProjectWebApi.Entities.Abstract
{
    public class ResearchFilterParam
    {
        public DateTime? PublishDate { get; set; }  // Yayınlanma tarihi
        public int? CategoryId { get; set; }  // Kategori ID
        public int PublishedBy { get; set; }  // Yazar
        public bool? IsFaceToFace { get; set; }  // Yüz yüze mi?
    }
}
