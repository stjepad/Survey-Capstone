using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.Models.AnonymousSurveyViewModels
{
    public class AnonymousSurveyViewModel
    {
        public Survey Survey { get; set; }

        public List<Question> Questions { get; set; }

        public List<int> AnswerInts{ get; set; }
        
        public List<AnswerSurveyInstance> Answers { get; set; }

        //public Dictionary<string, List<SelectListItem>> superMetaShit { get; set; }
    }
}
