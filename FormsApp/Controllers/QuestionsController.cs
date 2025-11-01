using FormsApp.Data;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FormsApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Questions?formId=5
        [AllowAnonymous]
        public async Task<IActionResult> Index(int formId)
        {
            var form = await _context.Forms.FindAsync(formId);
            if (form == null)
                return NotFound();

            var questions = await _context.Questions
                .Where(q => q.FormId == formId)
                .ToListAsync();

            ViewBag.Form = form;
            return View(questions);
        }

        // GET: /Questions/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions
                .Include(q => q.Options)
                .Include(q => q.Form)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
                return NotFound();

            return View(question);
        }

        // GET: /Questions/Create?formId=5
        public IActionResult Create(int formId)
        {
            var model = new QuestionCreateViewModel
            {
                FormId = formId
            };
            return View(model);
        }

        // POST: /Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Text = model.Text,
                    Type = model.Type,
                    IsRequired = model.IsRequired,
                    ImagePath = model.ImagePath,
                    MinValue = model.MinValue,
                    MaxValue = model.MaxValue,
                    Step = model.Step,
                    FormId = model.FormId
                };

                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Questions", new { formId = question.FormId });
            }
            return View(model);
        }

        // GET: /Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            return View(question);
        }

        // POST: /Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (id != question.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { formId = question.FormId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Questions.Any(e => e.Id == question.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(question);
        }

        // GET: /Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions
                .Include(q => q.Form)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
                return NotFound();

            return View(question);
        }

        // POST: /Questions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { formId = question?.FormId });
        }
    }
}
