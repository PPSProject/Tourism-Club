using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    interface IGids
    {
        IEnumerable<Gid> gids { get; }
        Gid getGid(int id);
        void addGid(Gid gid);
        void removeGid(int id);
    }
}
