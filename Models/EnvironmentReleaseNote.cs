using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class EnvironmentReleaseNote
    {
        public int EnvironmentId { get; set; }
        public int ReleaseNoteId { get; set; }

        public Environment Environment { get; set; }
        public ReleaseNote ReleaseNote { get; set; }
    }
}