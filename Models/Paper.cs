﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace Conference_Management_System.Models
{
    public class Paper
    {
        public Paper()
        {
            DatePublished = DateOnly.FromDateTime(DateTime.Now);
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Conference { get; set; }
        [Required]
        public String Expertises { get; set; }
        [Required]
        public DateOnly DatePublished { get; set; }
        [Required]
        [NotMapped]
        public IFormFile File { get; set; }    
    }
}
