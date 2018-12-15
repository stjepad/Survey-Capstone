using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public bool Published { get; set; }

        // Fk

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}