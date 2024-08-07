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
    public class SubjectController : ControllerBase
    {
        private readonly StudentengagementContext _context;

        public SubjectController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.Include(s => s.Course).ToListAsync();
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(string id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.SubjectCode == id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject([FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubject), new { id = subject.SubjectCode }, subject);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(string id, [FromBody] Subject subject)
        {
            if (id != subject.SubjectCode)
            {
                return BadRequest("SubjectCode mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Update(subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.SubjectCode))
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

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(string id)
        {
            return _context.Subjects.Any(e => e.SubjectCode == id);
        }
    }
}
