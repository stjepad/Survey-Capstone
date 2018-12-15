using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Data;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models.QandAViewModels
{
    public class CreateQuestionViewModel
    {

        public Survey Survey { get; set; }
        public Question Question { get; set; }

        [Required]
        public List<string> AnswerContents { get; set; }

    }
}
