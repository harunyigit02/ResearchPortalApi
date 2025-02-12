using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        public int ParticipantId { get; set; }

        public DateTime ParticipatedAt { get; set; }





        [JsonIgnore]

        public Question? Question { get; set; }
        [JsonIgnore]
        public Option? Option { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
