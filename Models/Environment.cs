using System;
using System.Collections.Generic;
using WebApi.Contracts;

namespace WebApi.Models
{
    public partial class Environment : IConfig
    {
        public Environment()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PropertyName { get { return "Environment"; } }
        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
