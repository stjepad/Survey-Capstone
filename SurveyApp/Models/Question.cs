using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public string Content { get; set; }

        // Fk 
        [Required]
        public int SurveyId { get; set; }

        
        public Survey Survey { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
