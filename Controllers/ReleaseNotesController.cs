using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseNotesController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

        public ReleaseNotesController(ReleaseNotesContext context)
        {
            _context = context;
        }

        // GET: api/ReleaseNotes
        [HttpGet]
        public IEnumerable<ReleaseNote> GetReleaseNote()
        {
            return _context.ReleaseNote;
        }

        // GET: api/ReleaseNotes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReleaseNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var releaseNote = await _context.ReleaseNote.FindAsync(id);

            if (releaseNote == null)
            {
                return NotFound();
            }

            return Ok(releaseNote);
        }

        // PUT: api/ReleaseNotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReleaseNote([FromRoute] int id, [FromBody] ReleaseNote releaseNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != releaseNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(releaseNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleaseNoteExists(id))
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

        // POST: api/ReleaseNotes
        [HttpPost]
        public async Task<IActionResult> PostReleaseNote([FromBody] ReleaseNote releaseNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReleaseNote.Add(releaseNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReleaseNote", new { id = releaseNote.Id }, releaseNote);
        }

        // DELETE: api/ReleaseNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReleaseNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var releaseNote = await _context.ReleaseNote.FindAsync(id);
            if (releaseNote == null)
            {
                return NotFound();
            }

            _context.ReleaseNote.Remove(releaseNote);
            await _context.SaveChangesAsync();

            return Ok(releaseNote);
        }

        private bool ReleaseNoteExists(int id)
        {
            return _context.ReleaseNote.Any(e => e.Id == id);
        }
    }
}