using System.ComponentModel.DataAnnotations;

namespace Conference_Management_System.Models
{
    public class Review
    {
        public Review()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string ReviewerName { get; set; }
        [Required]
        public int Paper { get; set; }
        public String Status { get; set; }
        public int Rating { get; set; } 
        public String Comments { get; set; }
    }
}
