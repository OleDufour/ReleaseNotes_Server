using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contracts
{
    public interface IConfig
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
