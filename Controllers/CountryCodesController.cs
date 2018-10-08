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
    public class CountryCodesController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

        public CountryCodesController(ReleaseNotesContext context)
        {
            _context = context;
        }

        // GET: api/CountryCodes
        [HttpGet]
        public IEnumerable<CountryCode> GetCountryCode()
        {
            return _context.CountryCode;
        }

        // GET: api/CountryCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryCode = await _context.CountryCode.FindAsync(id);

            if (countryCode == null)
            {
                return NotFound();
            }

            return Ok(countryCode);
        }

        // PUT: api/CountryCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryCode([FromRoute] int id, [FromBody] CountryCode countryCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryCodeExists(id))
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

        // POST: api/CountryCodes
        [HttpPost]
        public async Task<IActionResult> PostCountryCode([FromBody] CountryCode countryCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CountryCode.Add(countryCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryCode", new { id = countryCode.Id }, countryCode);
        }

        // DELETE: api/CountryCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryCode = await _context.CountryCode.FindAsync(id);
            if (countryCode == null)
            {
                return NotFound();
            }

            _context.CountryCode.Remove(countryCode);
            await _context.SaveChangesAsync();

            return Ok(countryCode);
        }

        private bool CountryCodeExists(int id)
        {
            return _context.CountryCode.Any(e => e.Id == id);
        }
    }
}