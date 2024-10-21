using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class ResearchRequirement
    {

        public int Id { get; set; }

        
        public int ResearchId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string EducationLevel { get; set; }
        public string Location { get; set; }
        public Research Research { get; set; }
    }
}
