using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class Answers
    {
        [Key]
        public int AnswersId { get; set; }

        [Required]
        public string Answer { get; set; }

        // Fk 
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }
    }
}
