using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;


namespace WebApi.Controllers
{
    //public class ReleaseNoteParms
    //{
    //    // public int ReleaseNoteId { get; set; }
    //    public int ReleaseId { get; set; }
    //    public int CleTypeId { get; set; }
    //    public int? CommentId { get; set; }
    //    public int[] CountryCodeId { get; set; }
    //    public int[] EnvironmentId { get; set; }
    //    public string KeyName { get; set; }
    //    public string Value { get; set; }
    //}


    [Route("api/[controller]")]
    [ApiController]
    public class _ReleaseNoteController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

        public _ReleaseNoteController(ReleaseNotesContext context)
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

            var ReleaseNote = await _context.ReleaseNote.FindAsync(id);

            if (ReleaseNote == null)
            {
                return NotFound();
            }

            return Ok(ReleaseNote);
        }

        public IQueryable<ReleaseNote> GetAllReleaseNotes()
        {
            var t = _context.ReleaseNote.AsQueryable(); //.Include("CountryCode")

            return t;
        }



      //  [HttpPost]
      //  [Route("SearchReleaseNotes")] // So the complete route is : /api/ReleaseNote/SearchReleaseNotes 
      //  public async Task<IEnumerable<ReleaseNoteParms>> SearchReleaseNotes(ReleaseNoteParms parms)
      //  {
      //      List<ReleaseNote> releaseNotesFound = await GetAllReleaseNotes().Where(r =>
      //       r.ReleaseId == parms.ReleaseId &&
      //       // r.EnvironmentId == parms.CleTypeId &&
      //       //  parms.CountryCodeId.Contains(r.CountryCodeId) &&
      //       //  parms.EnvironmentId.Contains(r.EnvironmentId) &&
      //       r.KeyName.Contains(parms.KeyName)
      //      ).ToListAsync();

      //      List<ReleaseNoteParms> lReleaseNotes = new List<ReleaseNoteParms>();
      //      var keys = releaseNotesFound.GroupBy(p => p.KeyName, (key) => new { key });
      ////      var countries = releaseNotesFound.GroupBy(p => p.CountryCodeId).Select(x => x.Key);

      //      //foreach (var k in keys)
      //      //{
      //      //    int CleTypeId = releaseNotesFound.Where(x => x.KeyName == k.Key).GroupBy(p => p.CleTypeId).Select(x => x.Key).First();
      //      //    int[] countryCodeId = releaseNotesFound.Where(x => x.KeyName == k.Key).GroupBy(p => p.CountryCodeId).Select(x => x.Key).ToArray();
      //      //    int[] environmentId = releaseNotesFound.Where(x => x.KeyName == k.Key).GroupBy(p => p.EnvironmentId).Select(x => x.Key).ToArray();
      //      //    string value = releaseNotesFound.Where(x => x.KeyName == k.Key).GroupBy(p => p.Value).Select(x => x.Key).First();
      //      //    lReleaseNotes.Add(new ReleaseNoteParms { KeyName = k.Key, Value = value, CountryCodeId = countryCodeId, EnvironmentId = environmentId });
      //      //}

      //      return lReleaseNotes;
      //  }

        // PUT: api/ReleaseNote/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReleaseNote([FromRoute] int id, [FromBody] ReleaseNote2 ReleaseNote)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != ReleaseNote.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ReleaseNote).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReleaseNoteExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ReleaseNote
        [HttpPost]
        public async Task<IActionResult> PostReleaseNote(dynamic json)
        {
            //Rootobject l = JsonConvert.DeserializeObject<Rootobject>(json.ToString());

            //for (int i = 0; i < l.releaseNoteArray.Length; i++)
            //{
            //    ReleaseNote r = l.releaseNoteArray[i];
            //    if (r.CountryCodeId == 0) throw new Exception("CountryCodeId==0");
            //    if (r.EnvironmentId == 0) throw new Exception("EnvironmentId==0");
            //    if (r.ReleaseId == 0) throw new Exception("ReleaseId==0");
            //    if (r.CleTypeId == 0) throw new Exception("CleTypeId==0");

            //    ReleaseNote rNewOrUpdate = await _context.ReleaseNote.SingleOrDefaultAsync(x =>
            //   x.CountryCodeId == r.CountryCodeId
            //   && x.EnvironmentId == r.EnvironmentId
            //   && x.ReleaseId == r.ReleaseId
            //   && x.CleTypeId == r.CleTypeId
            //   && x.KeyName == r.KeyName
            //    );

            //    if (rNewOrUpdate == null)
            //        _context.ReleaseNote.Add(r);
            //    else
            //    {
            //        rNewOrUpdate.Value = r.Value;// update existing value
            //    }
            //    await _context.SaveChangesAsync();
            //}
            return Ok();
        }

        //// POST: api/ReleaseNote
        //[HttpPost]
        //public async Task<IActionResult> PostReleaseNotes([FromBody] List<ReleaseNote> ReleaseNotes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    foreach (ReleaseNote rn in ReleaseNotes)
        //    {
        //        _context.ReleaseNote.Add(rn);
        //        await _context.SaveChangesAsync();
        //    }

        //    return CreatedAtAction("GetReleaseNote", new { id = 11111 }, ReleaseNotes[0]);
        //}


        // DELETE: api/ReleaseNote/omniture.url.blabla
        [HttpDelete("{keyName}")]
        public async Task<IActionResult> DeleteReleaseNote([FromRoute] string keyName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReleaseNote rDelete = await _context.ReleaseNote.SingleOrDefaultAsync(x => x.KeyName == keyName);


            if (rDelete != null)
            {

                _context.ReleaseNote.Remove(rDelete);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        private bool ReleaseNoteExists(int id)
        {
            return _context.ReleaseNote.Any(e => e.Id == id);
        }
    }
}