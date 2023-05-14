using System.ComponentModel.DataAnnotations;

namespace Conference_Management_System.Models
{
    public class Conference
    {
        public Conference()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public string ConferenceChair { get; set; }
        [Required]
        public string Expertise { get; set; }
        public DateOnly ConferenceDate { get; set; }

    }
}
