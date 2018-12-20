using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models.SurveyDetailsViewModels
{
    public class SruveyDetailsViewModel
    {
        public Survey Survey {get; set;}
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();

        //LETS TRY IT BABY!!!
        public List<AnswerSurveyInstance> AnswerSurveyInstance { get; set; } = new List<AnswerSurveyInstance>();
    }
}
