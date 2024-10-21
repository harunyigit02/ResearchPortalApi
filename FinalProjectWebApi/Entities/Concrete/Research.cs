using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Research
    {

        public int Id { get; set; }

        
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Status { get; set; }  
        public Category Category { get; set; }
    }
}
