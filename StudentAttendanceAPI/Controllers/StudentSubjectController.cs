using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceAPI.Models;

namespace StudentAttendanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {
        private readonly StudentengagementContext _context;

        public StudentSubjectController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: api/StudentSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSubject>>> GetStudentSubjects()
        {
            return await _context.StudentSubjects
                .Include(s => s.Student)
                .Include(s => s.SubjectCodeNavigation)
                .ToListAsync();
        }

        // GET: api/StudentSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSubject>> GetStudentSubject(int id)
        {
            var studentSubject = await _context.StudentSubjects
                .Include(s => s.Student)
                .Include(s => s.SubjectCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentSubject == null)
            {
                return NotFound();
            }

            return studentSubject;
        }

        // POST: api/StudentSubjects
        [HttpPost]
        public async Task<ActionResult<StudentSubject>> CreateStudentSubject([FromBody] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentSubject);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStudentSubject), new { id = studentSubject.Id }, studentSubject);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/StudentSubjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudentSubject(int id, [FromBody] StudentSubject studentSubject)
        {
            if (id != studentSubject.Id)
            {
                return BadRequest("StudentSubject ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSubjectExists(studentSubject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/StudentSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentSubject(int id)
        {
            var studentSubject = await _context.StudentSubjects.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            _context.StudentSubjects.Remove(studentSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentSubjectExists(int id)
        {
            return _context.StudentSubjects.Any(e => e.Id == id);
        }
    }
}
