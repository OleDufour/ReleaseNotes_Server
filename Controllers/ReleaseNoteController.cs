﻿using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseNoteController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

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

            var ReleaseNote = await _context.ReleaseNote.FindAsync(id);

            if (ReleaseNote == null)
            {
                return NotFound();
            }

            return Ok(ReleaseNote);
        }

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
            Rootobject l = JsonConvert.DeserializeObject<Rootobject>(json.ToString());

            for (int i = 0; i < l.releaseNoteArray.Length; i++)
            {
                ReleaseNote r = l.releaseNoteArray[i];
                if (r.CountryCodeId == 0) throw new Exception("CountryCodeId==0");
                if (r.EnvironmentId == 0) throw new Exception("EnvironmentId==0");
                if (r.ReleaseId == 0) throw new Exception("ReleaseId==0");
                if (r.CleTypeId == 0) throw new Exception("CleTypeId==0");

                ReleaseNote rNewOrUpdate = await _context.ReleaseNote.SingleOrDefaultAsync(x =>
               x.CountryCodeId == r.CountryCodeId
               && x.EnvironmentId == r.EnvironmentId
               && x.ReleaseId == r.ReleaseId
               && x.CleTypeId == r.CleTypeId
               && x.Key == r.Key
                );

                if (rNewOrUpdate == null)
                    _context.ReleaseNote.Add(r);
                else
                {
                    rNewOrUpdate.Value = r.Value;// update existing value
                }
                await _context.SaveChangesAsync();
            }
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


        // DELETE: api/ReleaseNote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReleaseNote([FromRoute] int id)
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

            _context.ReleaseNote.Remove(ReleaseNote);
            await _context.SaveChangesAsync();

            return Ok(ReleaseNote);
        }

        private bool ReleaseNoteExists(int id)
        {
            return _context.ReleaseNote.Any(e => e.Id == id);
        }
    }
}