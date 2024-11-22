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
        public int OptionId { get; set; }
        


        [JsonIgnore]
        public Option? Options { get; set; }

    }
}
