using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class ReleaseName
    {
        public ReleaseName()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }

        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
