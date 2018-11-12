using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class CleType
    {
        public CleType()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ReleaseNote> ReleaseNote { get; set; }
        public string PropertyName { get { return "CleType"; } }
    }
}
