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
    public class EFUsers : IUsers
    {
        private readonly AppDBContent context;
        public EFUsers(AppDBContent context)
        {
            this.context = context;
        }
        public IEnumerable<User> users => context.users;

        public void addUser(User user)
        {
            if (user.Id == default)
            {
                context.Entry(user).State = EntityState.Added;
            }
            else
            {
                context.Entry(user).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public User getUser(int id)
        {
            return context.users.FirstOrDefault(x => x.Id == id);
        }

        public void removeUser(int id)
        {
            context.users.Remove(new User() { Id = id });
            context.SaveChanges();
        }
    }
}
