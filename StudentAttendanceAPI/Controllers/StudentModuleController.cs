using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceAPI.Models;

namespace StudentAttendanceAPI.Controllers
{
    public class StudentModuleController : Controller
    {
        private readonly StudentengagementContext _context;

        public StudentModuleController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: StudentModule
        public async Task<IActionResult> Index()
        {
            var studentengagementContext = _context.StudentModules.Include(s => s.Student).Include(s => s.SubjectCodeNavigation);
            return View(await studentengagementContext.ToListAsync());
        }

        // GET: StudentModule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModule = await _context.StudentModules
                .Include(s => s.Student)
                .Include(s => s.SubjectCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModule == null)
            {
                return NotFound();
            }

            return View(studentModule);
        }

        // GET: StudentModule/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode");
            return View();
        }

        // POST: StudentModule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,SubjectCode")] StudentModule studentModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentModule.StudentId);
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", studentModule.SubjectCode);
            return View(studentModule);
        }

        // GET: StudentModule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModule = await _context.StudentModules.FindAsync(id);
            if (studentModule == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentModule.StudentId);
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", studentModule.SubjectCode);
            return View(studentModule);
        }

        // POST: StudentModule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,SubjectCode")] StudentModule studentModule)
        {
            if (id != studentModule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModuleExists(studentModule.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentModule.StudentId);
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", studentModule.SubjectCode);
            return View(studentModule);
        }

        // GET: StudentModule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentModule = await _context.StudentModules
                .Include(s => s.Student)
                .Include(s => s.SubjectCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentModule == null)
            {
                return NotFound();
            }

            return View(studentModule);
        }

        // POST: StudentModule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentModule = await _context.StudentModules.FindAsync(id);
            if (studentModule != null)
            {
                _context.StudentModules.Remove(studentModule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModuleExists(int id)
        {
            return _context.StudentModules.Any(e => e.Id == id);
        }
    }
}
