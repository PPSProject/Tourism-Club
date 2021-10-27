using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;
using Microsoft.EntityFrameworkCore;
using tourism_club.Domain.Interfaces;

namespace tourism_club.Domain.Classes
{
    public class EFLocations : ILocations
    {
        private readonly AppDBContent context;
        public EFLocations(AppDBContent context)
        {
            this.context = context;
        }
        public IEnumerable<Location> locations => context.locations;

        public Location getLocation(int id)
        {
            return context.locations.FirstOrDefault(x => x.Id == id);
            
        }

        public void addLocation(Location loc)
        {
            if(loc.Id == default)
            {
                 context.Entry(loc).State = EntityState.Added;
            }
            else
            {
                context.Entry(loc).State = EntityState.Modified;
            }
            //context.SaveChanges();
        }

        public void removeLocation(int id)
        {
            context.locations.Remove(new Location() { Id = id });
            context.SaveChanges();
        }
    }
}
