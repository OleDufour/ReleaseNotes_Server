using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AdministrateController : Controller
    {
        private readonly IMemoryCache _cache;
        public AdministrateController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [Route("GetFtpClients")]
        public IActionResult GetFtpClients()
        {
            List<FtpClient> ftpClients = (List<FtpClient>)_cache.Get("ftpClients");
            return Ok(ftpClients);
        }

        [Route("GetConfig")]
        public IActionResult GetConfig()
        {
            List<FtpClient> ftpClients = (List<FtpClient>)_cache.Get("ftpClients");
            return Ok(ftpClients);
        }


        [HttpGet("GetFtpClientByName/{name}")]
        public IActionResult GetFtpClientByName(string name)
        {
            FtpClient ftpClient = ((List<FtpClient>)_cache.Get("ftpClients")).Where(x=> x.Name==name).FirstOrDefault() ;
            List<FtpClient> ftpClients = new List<FtpClient> { ftpClient };
            return Ok(ftpClients);
        }


        [HttpPut("{name}")]
        public IActionResult Update(string name, [FromBody]FtpClient ftpClient)
        {
            List<FtpClient> l = (List<FtpClient>)_cache.Get("ftpClients");
            l.Where(x => x.Name == ftpClient.Name).ToList().ForEach(x => x.StorageMax = ftpClient.StorageMax);
            _cache.Set("ftpClients", l);
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody]FtpClient ftpClient) {
            List<FtpClient> l = (List<FtpClient>)_cache.Get("ftpClients");
            l.Add(ftpClient);
            _cache.Set("ftpClients", l);
            return Ok();
        }


        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            List<FtpClient> l = (List<FtpClient>)_cache.Get("ftpClients");
            l = l.Where(x => x.Name != name).ToList();
            _cache.Set("ftpClients", l);
            return Ok();
        }


    }
}