using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Interfaces
{
    interface ILocations
    {
        IEnumerable<Location> locations { get; }
        Location getLocation { get; }
        Location addLocation { set; }

    }
}
