using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Environment
    {
        public Environment()
        {
            EnvironmentReleaseNote = new HashSet<EnvironmentReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EnvironmentReleaseNote> EnvironmentReleaseNote { get; set; }
        public string PropertyName { get { return "Environment"; } }
    }
}
