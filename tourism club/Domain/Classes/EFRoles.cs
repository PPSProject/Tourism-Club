using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domain.Interfaces;
using tourism_club.Models;
using Microsoft.EntityFrameworkCore;

namespace tourism_club.Domain.Classes
{
    public class EFRoles : IRoles
    {
        private readonly AppDBContent context;
        public EFRoles(AppDBContent context)
        {
            this.context = context;
        }
        public Role getRole(User user)
        {
            return context.roles.FirstOrDefault(x => x.UserId == user.Id);
        }
        public void addRole(Role role)
        {
            if (role.Id == default)
            {
                context.Entry(role).State = EntityState.Added;
            }
            else
            {
                context.Entry(role).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
