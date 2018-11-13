using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ReleaseNotesContext _context;

        public ConfigController(ReleaseNotesContext context)
        {
            _context = context;
        }

        // GET: api/Config
        [HttpGet]
        public List<object> GetAll()
        {
            var release = _context.Release;
            var countryCode = _context.CountryCode;
            var environment = _context.Environment;
            var cleType = _context.CleType;

            List<object> l = release.Cast<object>()
               .Concat(countryCode)
               .Concat(environment)
               .Concat(cleType)
               .ToList();

             return l;
        }

    }
}