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
    public class StudentCourseController : ControllerBase
    {
        private readonly StudentengagementContext _context;

        public StudentCourseController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            return await _context.StudentCourses.Include(s => s.ModuleCodeNavigation).Include(s => s.Student).ToListAsync();
        }

        // GET: api/StudentCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int id)
        {
            var studentCourse = await _context.StudentCourses
                .Include(s => s.ModuleCodeNavigation)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentCourse == null)
            {
                return NotFound();
            }

            return studentCourse;
        }

        // POST: api/StudentCourses
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> CreateStudentCourse([FromBody] StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourse);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStudentCourse), new { id = studentCourse.Id }, studentCourse);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/StudentCourses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudentCourse(int id, [FromBody] StudentCourse studentCourse)
        {
            if (id != studentCourse.Id)
            {
                return BadRequest("StudentCourse ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseExists(studentCourse.Id))
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

        // DELETE: api/StudentCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourse(int id)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCourseExists(int id)
        {
            return _context.StudentCourses.Any(e => e.Id == id);
        }
    }
}
