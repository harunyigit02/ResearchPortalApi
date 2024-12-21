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
        public Gender Gender { get; set; }
        public Location Location { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public Occupation Occupation { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public ParentalStatus ParentalStatus { get; set; }
        public ChildStatus ChildStatus { get; set; }
        public DisabilityStatus DisabilityStatus { get; set; }
        public HousingType HousingType { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
