using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domains.Interfaces;
using tourism_club.Models;
using Microsoft.EntityFrameworkCore;

namespace tourism_club.Domain.Classes
{
    public class EFComments : IComments
    {
        private readonly AppDBContent context;

        public EFComments(AppDBContent context)
        {
            this.context = context;
        }
        public IEnumerable<Comment> comments
        {
            get
            {
                return context.comments;
            }
        }

        public void addComment(Comment comm)
        {
            if(comm.Id == default)
            {
                context.Entry(comm).State = EntityState.Added;
            }
            else
            {
                context.Entry(comm).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
