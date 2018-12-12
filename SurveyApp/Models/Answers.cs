using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public string Content { get; set; }

        // Fk 
        [Required]
        public int QuestionId { get; set; }

       
        public Question Question { get; set; }

        public virtual ICollection<AnswerSurveyInstance> AnswerSurveyInstances { get; set; }
    }
}
