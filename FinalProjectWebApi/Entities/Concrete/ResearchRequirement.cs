using FinalProjectWebApi.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class ResearchRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        
        public int ResearchId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public List<Gender>? Gender { get; set; }
        public List<Location>? Location { get; set; }
        public List<EducationLevel>? EducationLevel { get; set; }
        public List<Occupation>? Occupation { get; set; }
        public List<Ethnicity>? Ethnicity { get; set; }
        public List<MaritalStatus>? MaritalStatus { get; set; }
        public List<ParentalStatus>? ParentalStatus { get; set; }
        public List<ChildStatus>? ChildStatus { get; set; }
        public List<DisabilityStatus>? DisabilityStatus { get; set; }
        public List<HousingType>? HousingType { get; set; }

        [JsonIgnore]
        public Research? Research { get; set; }
    }
}
