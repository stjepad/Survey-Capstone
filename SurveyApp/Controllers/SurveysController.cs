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
using SurveyApp.Models.SurveyDetailsViewModels;

namespace SurveyApp.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public SurveysController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();

            List<Survey> surveys = await _context.Surveys.Where(s => s.UserId == user.Id).ToListAsync();

            return View(surveys);
        }

        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var survey = await _context.Surveys
                
                .Include(s => s.User)
                .Include(q => q.Questions)
                    .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        //Survey results
        // GET: Surveys/SurveyDetails/5
        public async Task<IActionResult> SurveyDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(s => s.User)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ThenInclude(a => a.AnswerSurveyInstances)
                .FirstOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            SruveyDetailsViewModel viewModel = new SruveyDetailsViewModel
            {
                Survey = survey,
                Questions = survey.Questions.ToList()
            };

            ViewData["scripts"] = new List<string>() {
                "ChartData"
            };

            return View(viewModel);
        }


        // GET: Surveys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Survey Survey)
        {

            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {

                Survey.User = await GetCurrentUserAsync();
                Survey.UserId = Survey.User.Id;

                _context.Add(Survey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Survey);
        }



        // GET: Surveys/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            var survey = await _context.Surveys.FindAsync(id);

            if (user.Id != survey.UserId)
            {
                return NotFound();
            }
           

            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Survey Survey)
        {


            ModelState.Remove("User");
            ModelState.Remove("UserId");


            if (ModelState.IsValid)
            {
                try
                {
                    Survey survey = await _context.Surveys.FindAsync(id);

                    survey.Name = Survey.Name;

                    _context.Update(survey);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(id))
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

            return View(Survey);
        }

        //publish update on main survey page.

        public async Task<IActionResult> Publish(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Survey survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            survey.Published = true;

            _context.Update(survey);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var survey = await _context.Surveys
                .Include(s => s.SurveyInstances)
                    .ThenInclude(si => si.AnswersSurveyInstances)
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Answers)

                .SingleOrDefaultAsync(s => s.SurveyId == id);

            foreach (SurveyInstance si in survey.SurveyInstances)
            {
                foreach (
                    AnswerSurveyInstance asi in si.AnswersSurveyInstances)
                {
                    _context.Remove(asi);
                }
                _context.Remove(si);
            }

            foreach (Question q in survey.Questions)
            {
                foreach (
                    Answer a in q.Answers
                    )
                {
                    _context.Remove(a);
                }
                _context.Remove(q);
            }



            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Surveys");


            //var question = await _context.Questions.Include(q => q.Answers).Include(q => q.Survey).SingleOrDefaultAsync(q => q.QuestionId == id);
            //foreach (Answer answer in question.Answers)
            //{
            //    _context.Remove(answer);
            //}


            //var Survey = await _context.Surveys.Include(q => q.Questions).SingleOrDefaultAsync(q => q.SurveyId == id);
            //foreach (Question question1 in Survey.Questions)
            //{
            //    _context.Remove(question1);
            //}



            var survey1 = await _context.Surveys.FindAsync(id);
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.SurveyId == id);
        }
    }
}
