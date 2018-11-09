using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class CountryCodeReleaseNote
    {
        public int CountryCodeId { get; set; }
        public int ReleaseNoteId { get; set; }

        public CountryCode CountryCode { get; set; }
        public ReleaseNote ReleaseNote { get; set; }
    }
}
