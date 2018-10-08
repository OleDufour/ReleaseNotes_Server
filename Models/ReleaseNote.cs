using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class ReleaseNote
    {
        public int Id { get; set; }
        public int CountryCodeId { get; set; }
        public int EnvironmentId { get; set; }
        public int CleTypeId { get; set; }
        public int ReleaseNameId { get; set; }
        public string Value { get; set; }

        public CleType CleType { get; set; }
        public CountryCode CountryCode { get; set; }
        public Environment Environment { get; set; }
        public ReleaseName ReleaseName { get; set; }
    }
}
