using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class CommentSection
    {
        public CommentSection()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
