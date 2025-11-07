using FormsApp.Data;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Controllers
{
    public class OptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Options?questionId=5
        public async Task<IActionResult> Index(int questionId)
        {
            var question = await _context.Questions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
                return NotFound();

            // Safety guard: only valid for SingleChoice or MultipleChoice
            if (question.Type != "SingleChoice" && question.Type != "MultipleChoice")
                return BadRequest("Options can only be managed for choice-based questions.");

            ViewBag.QuestionId = questionId;
            ViewBag.QuestionText = question.Text;

            return View(question.Options?.ToList() ?? new List<Option>());
        }

        // GET: /Options/Create?questionId=5
        public IActionResult Create(int questionId)
        {
            var model = new OptionCreateViewModel
            {
                QuestionId = questionId
            };

            return View(model);
        }

        // POST: /Options/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OptionCreateViewModel model)
        {
            var question = await _context.Questions.FindAsync(model.QuestionId);
            if (question == null)
                return NotFound();

            if (question.Type != "SingleChoice" && question.Type != "MultipleChoice")
                return BadRequest("This question type does not support options.");

            if (ModelState.IsValid)
            {
                var option = new Option
                {
                    Text = model.Text,
                    ImagePath = model.ImagePath,
                    QuestionId = model.QuestionId
                };

                _context.Options.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { questionId = model.QuestionId });
            }

            return View(model); // returns the view model, not the entity
        }



        // GET: /Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var option = await _context.Options.FindAsync(id);
            if (option == null) return NotFound();

            var model = new OptionCreateViewModel
            {
                Id = option.Id,
                Text = option.Text,
                ImagePath = option.ImagePath,
                QuestionId = option.QuestionId
            };

            return View(model); // passes the view model to the strongly typed view
        }


        // POST: /Options/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OptionCreateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var option = await _context.Options.FindAsync(id);
                if (option == null) return NotFound();

                // Update entity
                option.Text = model.Text;
                option.ImagePath = model.ImagePath;

                _context.Update(option);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { questionId = option.QuestionId });
            }

            // If ModelState invalid, return the view with the same model
            return View(model);
        }


        // GET: /Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var option = await _context.Options
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (option == null) return NotFound();

            return View(option);
        }

        // POST: /Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option != null)
            {
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { questionId = option?.QuestionId });
        }
    }
}
