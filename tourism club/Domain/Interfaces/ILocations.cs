using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domains.Interfaces
{
    interface ILocations
    {
        IEnumerable<Location> locations { get; }
        Location getLocation(int id);
        void addLocation(Location loc);
        void removeLocation(int id);

    }
}
