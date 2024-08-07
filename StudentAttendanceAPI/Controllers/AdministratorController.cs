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
    public class AdministratorController : ControllerBase
    {
        private readonly StudentengagementContext _context;

        public AdministratorController(StudentengagementContext context)
        {
            _context = context;
        }

        // GET: api/Administrator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrators()
        {
            return await _context.Administrators.Include(a => a.Campus).ToListAsync();
        }

        // GET: api/Administrator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .Include(a => a.Campus)
                .FirstOrDefaultAsync(m => m.AdministratorId == id);

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        // POST: api/Administrator
        [HttpPost]
        public async Task<ActionResult<Administrator>> CreateAdministrator([FromBody] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrator);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAdministrator), new { id = administrator.AdministratorId }, administrator);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Administrator/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdministrator(string id, [FromBody] Administrator administrator)
        {
            if (id != administrator.AdministratorId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministratorExists(administrator.AdministratorId))
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

        // DELETE: api/Administrator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .Include(a => a.Campus)
                .FirstOrDefaultAsync(m => m.AdministratorId == id);

            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministratorExists(string id)
        {
            return _context.Administrators.Any(e => e.AdministratorId == id);
        }
    }
}
