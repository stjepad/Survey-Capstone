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

namespace SurveyApp.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public AnswersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers.Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,Content,QuestionId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Content", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            var answer = await _context.Answers
                .Include(a => a.Question)
                    .ThenInclude(q => q.Survey)
                .SingleOrDefaultAsync(a => a.AnswerId == id);

            if (user.Id != answer.Question.Survey.UserId)
            {
                return NotFound();
            }


            if (answer == null)
            {
                return NotFound();
            }
           
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Answer Answer)
        {

            ModelState.Remove("QuestionId");

            if (ModelState.IsValid)
            {
                Answer answer = await _context.Answers.Include(a => a.Question).SingleOrDefaultAsync(a => a.AnswerId == id);

                try
                {

                    answer.Content = Answer.Content;

                    _context.Update(answer);
                    await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Surveys", new { id = answer.Question.SurveyId });
            }
           
            return View(Answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}
