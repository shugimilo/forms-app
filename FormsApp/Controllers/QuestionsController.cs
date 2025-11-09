using FormsApp.Data;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ------------------------------------------------------------
        // GET: /Questions?formId=5
        // ------------------------------------------------------------
        public async Task<IActionResult> Index(int formId)
        {
            var questions = await _context.Questions
                .Where(q => q.FormId == formId)
                .Include(q => q.Options)
                .ToListAsync();

            var form = _context.Forms.Find(formId);
            if (form == null) return NotFound();

            ViewBag.Form = form;

            ViewBag.FormId = formId; // optional, for your view
            return View(questions);
        }

        // ------------------------------------------------------------
        // GET: /Questions/Details/5
        // ------------------------------------------------------------
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

        // ------------------------------------------------------------
        // GET: /Questions/Create?formId=5
        // ------------------------------------------------------------
        public IActionResult Create(int formId)
        {
            var model = new QuestionCreateViewModel
            {
                FormId = formId
            };

            return View(model);
        }

        // ------------------------------------------------------------
        // POST: /Questions/Create
        // ------------------------------------------------------------
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

                return RedirectToAction(nameof(Index), new { formId = model.FormId });
            }

            return View(model);
        }

        // ------------------------------------------------------------
        // GET: /Questions/Edit/5
        // ------------------------------------------------------------
        public async Task<IActionResult> Edit(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("UserId"))
                return RedirectToAction("Login", "Account");
            if (id == null)
                return NotFound();

            var question = await _context.Questions
                .Include(q => q.Form)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
                return NotFound();

            var currentUserId = HttpContext.Session.GetString("UserId");
            if (currentUserId == null || question.Form.UserId != int.Parse(currentUserId))
                return Forbid();

            var model = new QuestionEditViewModel
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type,
                IsRequired = question.IsRequired,
                ImagePath = question.ImagePath,
                MinValue = question.MinValue,
                MaxValue = question.MaxValue,
                Step = question.Step,
                FormId = question.FormId
            };

            return View(model);
        }

        // ------------------------------------------------------------
        // POST: /Questions/Edit/5
        // ------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuestionEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var question = await _context.Questions.FindAsync(model.Id);
                    if (question == null) return NotFound();
                    question.Text = model.Text;
                    question.Type = model.Type;
                    question.IsRequired = model.IsRequired;
                    question.ImagePath = model.ImagePath;
                    question.MinValue = model.MinValue;
                    question.MaxValue = model.MaxValue;
                    question.Step = model.Step;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { formId = question.FormId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Questions.Any(e => e.Id == model.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // ------------------------------------------------------------
        // GET: /Questions/Delete/5
        // ------------------------------------------------------------
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

        // ------------------------------------------------------------
        // POST: /Questions/Delete/5
        // ------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
