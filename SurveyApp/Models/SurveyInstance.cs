using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class SurveyInstance
    {
        [Key]
        public int SurveyInstanceId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        // Fk 

        [Required]
        public int SurveyId { get; set; }

        [Required]
        public Survey Survey { get; set; }

        public virtual ICollection<AnswerSurveyInstance> AnswersSurveyInstances { get; set; }
    }
}
