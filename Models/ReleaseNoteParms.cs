using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ReleaseNoteParms
    {
        // public int ReleaseNoteId { get; set; }
        public int ReleaseId { get; set; }
        public int CleTypeId { get; set; }
        public int[] CountryCodeId { get; set; }
        public int[] EnvironmentId { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }

}
