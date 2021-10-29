using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using tourism_club.Domains.Interfaces;
using tourism_club.Models;
using Microsoft.EntityFrameworkCore;
using tourism_club.Domain.Interfaces;

namespace tourism_club.Domain.Classes
{
    public class EFGids:IGids
    {
        private readonly AppDBContent context;
        public EFGids(AppDBContent context)
        {
            this.context = context;
        }

        public IEnumerable<Gid> gids => context.gids;

        public void addGid(Gid gid)
        {
            if (gid.Id == default)
            {
                context.Entry(gid).State = EntityState.Added;
            }
            else
            {
                context.Entry(gid).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Gid getGid(int id)
        {
            return context.gids.FirstOrDefault(x => x.Id == id);
        }

        public void removeGid(int id)
        {
            context.gids.Remove(new Gid() { Id = id });
            context.SaveChanges();
        }
    }
}
