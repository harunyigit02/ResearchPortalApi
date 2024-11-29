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
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public List<string> Gender { get; set; }
        public List<string> Location { get; set; }
        public List<string> EducationLevel { get; set; }
        public List<string> Occupation { get; set; }
        public List<string> Ethnicity { get; set; }
        public List<string> MaritalStatus { get; set; }
        public List<string> ParentalStatus { get; set; }
        public List<string> ChildStatus { get; set; }
        public List<string> DisabilityStatus { get; set; }
        public List<string> HousingType { get; set; }

        [JsonIgnore]
        public Research? Research { get; set; }
    }
}
