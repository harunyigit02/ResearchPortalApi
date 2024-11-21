using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Research
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public int PublishedBy { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? Status { get; set; }

        [Required]
        public bool IsFaceToFace { get; set; }
        public bool IsCompleted { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public User? User { get; set; }


        public ICollection<Question>? Questions { get; set; }
    }
}
