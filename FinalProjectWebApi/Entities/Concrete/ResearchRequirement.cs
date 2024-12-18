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
        
        
       


        public virtual Research? Research { get; set; }
        

    }
}
