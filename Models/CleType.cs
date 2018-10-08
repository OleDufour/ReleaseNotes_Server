using System;
using System.Collections.Generic;
using WebApi.Contracts;

namespace WebApi.Models
{
    public partial class CleType:IConfig
    {
        public CleType()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string PropertyName { get { return "CleType"; } }

        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
