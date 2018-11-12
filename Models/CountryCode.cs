using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class CountryCode
    {
        public CountryCode()
        {
            CountryCodeReleaseNote = new HashSet<CountryCodeReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CountryCodeReleaseNote> CountryCodeReleaseNote { get; set; }
        public string PropertyName { get { return "CountryCode"; } }
    }
}
