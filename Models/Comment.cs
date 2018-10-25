using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Comment
    {
        public Comment()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
