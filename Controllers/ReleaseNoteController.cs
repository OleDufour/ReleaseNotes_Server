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
    public class ReleaseNoteParms
    {
        public int ReleaseNoteId { get; set; }
        public int ReleaseId { get; set; }
        public int CleTypeId { get; set; }
        public int? CommentId { get; set; }
        public int[] CountryCodeId { get; set; }
        public int[] EnvironmentId { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }




    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseNoteController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

        ReleaseNote FindUniqueReleaseNote(List<ReleaseNote> releaseNotes, int releaseId, int cleTypeId, string keyName)
        {
            ReleaseNote rn = releaseNotes.Where(x => x.ReleaseId == releaseId && x.CleTypeId == cleTypeId && x.KeyName == keyName).FirstOrDefault();
            return rn;
        }

        public ReleaseNoteController(ReleaseNotesContext context)
        {
            _context = context;
        }

        // GET: api/ReleaseNote
        [HttpGet]
        public IEnumerable<ReleaseNote> GetReleaseNote()
        {
            return _context.ReleaseNote;
        }

        // GET: api/ReleaseNote/5
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

        // PUT: api/ReleaseNote/5
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

        // POST: api/ReleaseNote
        [HttpPost]
        public async Task<IActionResult> PostReleaseNote(ReleaseNoteParms rnp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReleaseNote releaseNote = new ReleaseNote();


            var rn = await _context.ReleaseNote.Where(x => x.KeyName == rnp.KeyName && x.ReleaseId == rnp.ReleaseId && rnp.CleTypeId == rnp.CleTypeId).Include("CountryCodeReleaseNote").Include("EnvironmentReleaseNote").SingleOrDefaultAsync();

            if (rnp.ReleaseNoteId == 0 && rn == null)
            {
                releaseNote = new ReleaseNote { KeyName = rnp.KeyName, Value = rnp.Value, CleTypeId = rnp.CleTypeId, ReleaseId = rnp.ReleaseId };
                foreach (int id in rnp.CountryCodeId)
                    releaseNote.CountryCodeReleaseNote.Add(new CountryCodeReleaseNote { CountryCodeId = id });

                foreach (int id in rnp.EnvironmentId)
                    releaseNote.EnvironmentReleaseNote.Add(new EnvironmentReleaseNote { EnvironmentId = id });

                _context.ReleaseNote.Add(releaseNote);
            }
            else
            { // TODO 
              // var rn = await _context.ReleaseNote.Where(x => x.KeyName == rnp.KeyName && x.ReleaseId == rnp.ReleaseId && rnp.CleTypeId == rnp.CleTypeId).Include("CountryCodeReleaseNote").Include("EnvironmentReleaseNote").SingleOrDefaultAsync();
                if (rnp.ReleaseNoteId != 0)
                    rn = await _context.ReleaseNote.Where(x => x.Id == rnp.ReleaseNoteId).Include("CountryCodeReleaseNote").Include("EnvironmentReleaseNote").SingleAsync();

                _context.CountryCodeReleaseNote.RemoveRange(rn.CountryCodeReleaseNote);
                _context.EnvironmentReleaseNote.RemoveRange(rn.EnvironmentReleaseNote);

                foreach (int id in rnp.CountryCodeId)
                    rn.CountryCodeReleaseNote.Add(new CountryCodeReleaseNote { CountryCodeId = id });

                foreach (int id in rnp.EnvironmentId)
                    rn.EnvironmentReleaseNote.Add(new EnvironmentReleaseNote { EnvironmentId = id });

                rn.Value = rnp.Value;
                rn.CleTypeId = rnp.CleTypeId; // The same key cannot occur in tech and func ?? yes !! maar waarschuwing !!!
                rn.CommentId = rnp.CommentId;
            }


            await _context.SaveChangesAsync();


            return Ok();
            //   return CreatedAtAction("GetReleaseNote", new { id = rn.Id }, rn);
        }


        public IQueryable<ReleaseNote> GetAllReleaseNotes()
        {
            var t = _context.ReleaseNote.Include("CountryCodeReleaseNote").Include("EnvironmentReleaseNote").AsQueryable(); //.Include("CountryCode")

            return t;
        }


        [HttpPost]
        [Route("SearchReleaseNotes")] // So the complete route is : /api/ReleaseNote/SearchReleaseNotes 
        public async Task<IEnumerable<ReleaseNoteParms>> SearchReleaseNotes(ReleaseNoteParms parms)
        {
            List<ReleaseNote> releaseNotesFound = await GetAllReleaseNotes().Where(r =>
             r.ReleaseId == parms.ReleaseId &&
             // r.EnvironmentId == parms.CleTypeId &&
             //  parms.CountryCodeId.Contains(r.CountryCodeId) &&
             //  parms.EnvironmentId.Contains(r.EnvironmentId) &&
             r.KeyName.Contains(parms.KeyName)
            ).ToListAsync();

            List<ReleaseNoteParms> lReleaseNotes = new List<ReleaseNoteParms>();
            var keys = releaseNotesFound.GroupBy(p => p.KeyName, (key) => new { key });
            var countries = releaseNotesFound.GroupBy(p => p.CountryCodeReleaseNote).Select(x => x.Key);

            foreach (var rnf in releaseNotesFound)
            {
                int CleTypeId = rnf.CleTypeId;
                //     var rn = FindUniqueReleaseNote(releaseNotesFound, parms.ReleaseId, parms.CleTypeId, parms.KeyName);
                int[] countryCodeId = rnf.CountryCodeReleaseNote.Select(x => x.CountryCodeId).OrderBy(x => x).ToArray();
                int[] environmentId = rnf.EnvironmentReleaseNote.Select(x => x.EnvironmentId).OrderBy(x => x).ToArray();


                //  string value = releaseNotesFound.Where(x => x.KeyName == k.Key).GroupBy(p => p.Value).Select(x => x.Key).First();
                lReleaseNotes.Add(new ReleaseNoteParms { ReleaseNoteId = rnf.Id, KeyName = rnf.KeyName, Value = rnf.Value, CountryCodeId = countryCodeId, EnvironmentId = environmentId, CleTypeId = rnf.CleTypeId });
            }

            return lReleaseNotes;
        }



        // DELETE: api/ReleaseNote/5
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