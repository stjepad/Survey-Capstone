using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models
{
    public class AnswerSurveyInstance
    {
        [Key]
        public int AnswerSurveyInstanceId { get; set; }

        [Required]
        public int SurveyInstanceId { get; set; }

        public SurveyInstance SurveyInstance { get; set; }

        // Fk

        [Required]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
