using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Views
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        
        public int ViewedById { get; set; }

        
        public int ViewedArticle {  get; set; }
        public DateTime ViewedAt { get; set; }

        public Article? Article { get; set; }
    }
}
