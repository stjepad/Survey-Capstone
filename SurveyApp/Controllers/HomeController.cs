using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models;
using SurveyApp.Models.AnonymousSurveyViewModels;

namespace SurveyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Surveys.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: take an annonymous survey

        //this is where I get the information from the database in order to pass it to AnonymousSurvey.cshtml to be displayed on the DOM
        public async Task<IActionResult> AnonymousSurvey(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            // Here I .Include the User (though user is not needed), questions, and answer as well as the SurveyId, so that I can use its information to build the view for the Anonymous user.
            var survey = await _context.Surveys
                .Include(s => s.User)
                .Include(s => s.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            // Here I reach into the the AnonymousSurveyViewModel and use the Survey and Questions list.
            AnonymousSurveyViewModel viewModel = new AnonymousSurveyViewModel
            {
                Survey = survey,
                Questions = survey.Questions.ToList()
            };

            // Here I Create a dictionary that holds a string and a List od SelectListItems
            Dictionary<string, List<SelectListItem>> placeHolder = new Dictionary<string, List<SelectListItem>>();

            //Here I loop over the Questions
            foreach (Question q in survey.Questions)
            {
                // Here I create a list of Select list items named answerOptions
                List<SelectListItem> answerOptions = new List<SelectListItem>();

                // Here I loop through the question answers and creating a select list item with the text=content and the value=AnswerId
                foreach (Answer a in q.Answers)
                {
                    // Here is the creation of each SelectListItem that represents each question answer
                    SelectListItem sli = new SelectListItem();
                    sli.Text = a.Content;
                    sli.Value = a.AnswerId.ToString();

                    answerOptions.Add(sli);
                }

                //Here we add the question content from above and the answerOptions that holds the answer content and AnswerId to placeHolder
                placeHolder.Add(q.Content, answerOptions);
                
            }


            // we equate placeHolder with superMetaShit in the AnonymousSurveyViewModal
            viewModel.superMetaShit = placeHolder;
           

            return View(viewModel);
        }


        //post
        [HttpPost]
        public async Task<IActionResult> AnonymousSurvey(int id, AnonymousSurveyViewModel viewModel)
        {
            SurveyInstance AnonymousSurvey = new SurveyInstance();
            AnonymousSurvey.SurveyId = id;
            AnonymousSurvey.DateCreated = DateTime.Now;
            _context.Add(AnonymousSurvey);

            foreach (AnswerSurveyInstance answer in viewModel.Answers)
            {
                answer.SurveyInstanceId = AnonymousSurvey.SurveyInstanceId;
                _context.Add(answer);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}