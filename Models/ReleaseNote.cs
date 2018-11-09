using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class ReleaseNote
    {
        public ReleaseNote()
        {
            CountryCodeReleaseNote = new HashSet<CountryCodeReleaseNote>();
            EnvironmentReleaseNote = new HashSet<EnvironmentReleaseNote>();
        }

        public int Id { get; set; }
        public int CountryCodeId { get; set; }
        public int EnvironmentId { get; set; }
        public int CleTypeId { get; set; }
        public int ReleaseId { get; set; }
        public int? CommentId { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }

        public CleType CleType { get; set; }
        public Release Release { get; set; }
        public ICollection<CountryCodeReleaseNote> CountryCodeReleaseNote { get; set; }
        public ICollection<EnvironmentReleaseNote> EnvironmentReleaseNote { get; set; }
    }
}
