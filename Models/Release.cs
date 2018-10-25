using System;
using System.Collections.Generic;
using WebApi.Contracts;

namespace WebApi.Models
{
    public partial class Release : IConfig
    {
        public Release()
        {
            ReleaseNote = new HashSet<ReleaseNote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PropertyName { get { return "Release"; } }
        public ICollection<ReleaseNote> ReleaseNote { get; set; }
    }
}
