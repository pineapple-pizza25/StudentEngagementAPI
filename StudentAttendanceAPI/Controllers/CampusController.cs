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
    public class CampusController : ControllerBase
    {
        private readonly StudentengagementContext _context;

        public CampusController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: api/Campus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campus>>> GetCampuses()
        {
            return await _context.Campuses.ToListAsync();
        }

        // GET: api/Campus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campus>> GetCampus(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FirstOrDefaultAsync(m => m.Id == id);

            if (campus == null)
            {
                return NotFound();
            }

            return campus;
        }

        // POST: api/Campus
        [HttpPost]
        public async Task<ActionResult<Campus>> CreateCampus([FromBody] Campus campus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCampus), new { id = campus.Id }, campus);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Campus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCampus(int id, [FromBody] Campus campus)
        {
            if (id != campus.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.Id))
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

        // DELETE: api/Campus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampus(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FirstOrDefaultAsync(m => m.Id == id);

            if (campus == null)
            {
                return NotFound();
            }

            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampusExists(int id)
        {
            return _context.Campuses.Any(e => e.Id == id);
        }
    }
}
