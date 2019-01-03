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
using SurveyApp.Models.QandAViewModels;

namespace SurveyApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public QuestionsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Questions.Include(q => q.Survey);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Survey)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public async Task<IActionResult> Create(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            Survey survey = await _context.Surveys.SingleOrDefaultAsync(s => s.SurveyId == id);

            CreateQuestionViewModel viewmodel = new CreateQuestionViewModel();

            if (survey.UserId != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(viewmodel);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, CreateQuestionViewModel viewmodel)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            viewmodel.Question.SurveyId = id;

            if (ModelState.IsValid)
            {
                _context.Add(viewmodel.Question);

                List<Answer> Answers = new List<Answer>();

                foreach (string answer in viewmodel.AnswerContents)
                {
                    Answer Answer = new Answer
                    {
                        Content = answer,
                        QuestionId = viewmodel.Question.QuestionId
                    };

                    Answers.Add(Answer);
                }

                foreach (Answer answer in Answers)
                {
                    _context.Add(answer);
                }




                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Surveys", new { id = id });

            }
            return View("Index", "Home");

            // my new code
            //if (!_context.Questions.Any())
            //{
            //    var Questions = new Question[]
            //    {
            //        new Question {
            //            Content = "Electronics"
            //        }
            //    };

            //    foreach (Question i in Questions)
            //    {
            //        _context.Questions.Add(i);
            //    }
            //    _context.SaveChanges();



            //    if (!_context.Answers.Any())
            //    {
            //        var Answers = new Answer[]
            //        {
            //        new Answer {
            //            Content = "Kite",
            //            Question = _context.Questions.Single(t => t.Content == "Sporting Goods")
            //        }
            //        };

            //        foreach (Answer i in Answers)
            //        {
            //            _context.Answers.Add(i);
            //        }

            //        _context.SaveChanges();
            //    }

            //}
            //end my new code

            //if (ModelState.IsValid)
            //{
            //    _context.Add(question);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "Name", question.SurveyId);
            //return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "Name", question.SurveyId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Content,SurveyId")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "Name", question.SurveyId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Survey)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         

            //code to remove answerFK
            var question = await _context.Questions.Include(q => q.Answers).Include(q => q.Survey).SingleOrDefaultAsync(q => q.QuestionId == id);
            foreach (Answer answer in question.Answers)
            {
                _context.Remove(answer);
            }



            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Surveys", new { id = question.Survey.SurveyId });
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
