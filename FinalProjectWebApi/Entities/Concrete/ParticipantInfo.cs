using FinalProjectWebApi.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class ParticipantInfo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string EducationLevel { get; set; }
        public string Occupation { get; set; }
        public string Ethnicity { get; set; }
        public string MaritalStatus { get; set; }
        public string ParentalStatus { get; set; }
        public string ChildStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string HousingType { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
