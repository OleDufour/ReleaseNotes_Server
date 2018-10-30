using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
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
            List<IConfig> l = new List<IConfig>();

            var release = _context.Release as IEnumerable<IConfig>;
            var countryCode = _context.CountryCode as IEnumerable<IConfig>;
            var environment = _context.Environment as IEnumerable<IConfig>;
            var cleType = _context.CleType as IEnumerable<IConfig>;

            l.AddRange(release.ToList());
            l.AddRange(countryCode.ToList());
            l.AddRange(environment.ToList());
            l.AddRange(cleType.ToList());

            var release2 = _context.Release;
            var countryCode2 = _context.CountryCode;
            var environment2 = _context.Environment;
            var cleType2 = _context.CleType;

            List<object> l2 = release2.Cast<object>()
               .Concat(countryCode2)
               .Concat(environment2)
               .Concat(cleType2)
               .ToList();
          




            return l2;
        }

    }
}