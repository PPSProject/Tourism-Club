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
    public class EFAdmins:IAdmins
    {
        private readonly AppDBContent context;
        public EFAdmins(AppDBContent context)
        {
            this.context = context;
        }

        public IEnumerable<Admin> admins => context.admins;

        public void addAdmin(Admin admin)
        {
            if (admin.Id == default)
            {
                context.Entry(admin).State = EntityState.Added;
            }
            else
            {
                context.Entry(admin).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Admin getAdmin(int id)
        {
            return context.admins.FirstOrDefault(x => x.Id == id);
        }

        public void removeAdmin(int id)
        {
            context.admins.Remove(new Admin() { Id = id });
            context.SaveChanges();
        }
    }
}
