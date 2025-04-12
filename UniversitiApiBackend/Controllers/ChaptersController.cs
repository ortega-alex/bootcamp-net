

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitiApiBackend.DataAccess;
using UniversitiApiBackend.Models.DataModels;

namespace UniversitiApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        public ChaptersController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapters>>> GetIndexes()
        {
            return await _context.Indexes.ToListAsync();
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapters>> GetChapters(int id)
        {
            var chapters = await _context.Indexes.FindAsync(id);

            if (chapters == null)
                return NotFound();

            return chapters;
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapters(int id, Chapters chapters)
        {
            if (id != chapters.Id)
                return BadRequest();

            _context.Entry(chapters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChaptersExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chapters>> PostChapters(Chapters chapters)
        {
            _context.Indexes.Add(chapters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapters", new { id = chapters.Id }, chapters);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapters(int id)
        {
            var chapters = await _context.Indexes.FindAsync(id);
            if (chapters == null)
                return NotFound();

            _context.Indexes.Remove(chapters);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChaptersExists(int id)
        {
            return _context.Indexes.Any(e => e.Id == id);
        }
    }
}
