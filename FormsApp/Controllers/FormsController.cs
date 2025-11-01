using Microsoft.AspNetCore.Mvc;
using FormsApp.Data;
using FormsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FormsApp.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Forms
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            IQueryable<Form> query = _context.Forms.Include(f => f.Questions);

            if (string.IsNullOrEmpty(currentUserId))
            {
                query = query.Where(f => !f.RequireLogin);
            }
            else
            {
                int userId = int.Parse(currentUserId);
                query = query.Where(f => !f.RequireLogin || f.UserId == userId);
            }
            
            var forms = await query.ToListAsync();
            return View(forms);
        }

        // GET: /Forms/Create
        public IActionResult Create()
        {
            if(!HttpContext.Session.Keys.Contains("UserId"))
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Forms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Form form)
        {
            if (ModelState.IsValid)
            {
                form.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                form.CreatedAt = DateTime.Now;

                _context.Forms.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: /Forms/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var form = await _context.Forms
                .Include(f => f.Questions)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (form == null)
                return NotFound();

            var currentUserId = HttpContext.Session.GetString("UserId");


            if (form.RequireLogin && string.IsNullOrEmpty(currentUserId))
                return RedirectToAction("Login", "Account");

            return View(form);
        }

        // GET: /Forms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("UserId"))
                return RedirectToAction("Login", "Account");

            if (id == null)
                return NotFound();

            var form = await _context.Forms.FindAsync(id);
            if (form == null)
                return NotFound();

            var currentUserId = HttpContext.Session.GetString("UserId");
            if (currentUserId == null || form.UserId != int.Parse(currentUserId))
                return Forbid();

            var model = new FormEditViewModel
            {
                Id = form.Id,
                Title = form.Title,
                Description = form.Description,
                RequireLogin = form.RequireLogin
            };

            return View(model);
        }

        // POST: /Forms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var form = await _context.Forms.FindAsync(model.Id);
                    if (form == null) return NotFound();

                    form.Title = model.Title;
                    form.Description = model.Description;
                    form.RequireLogin = model.RequireLogin;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Forms.Any(e => e.Id == model.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("UserId"))
                return RedirectToAction("Login", "Account");

            if (id == null)
                return NotFound();

            var form = await _context.Forms
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null)
                return NotFound();

            return View(form);
        }

        // POST: /Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form != null)
            {
                _context.Forms.Remove(form);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
