﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinalProjectWebApi.Entities.Concrete
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? Content { get; set; }
        public int PublishedBy { get; set; }
        public DateTime PublishedAt { get; set; }
        public int TotalViews { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Category? Category { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
