using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ResearchId { get; set; } // Hangi araştırmaya ait olduğu
        public string QuestionText { get; set; }

        // İlişki

        [JsonIgnore]
        [NotMapped]
        public Research? Research { get; set; }

        // Seçenekleri tutmak için ICollection
        public ICollection<Option>? Options { get; set; }
    }
}
