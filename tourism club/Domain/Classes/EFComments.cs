using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;
using Microsoft.EntityFrameworkCore;
using tourism_club.Domain.Interfaces;

namespace tourism_club.Domain.Classes
{
    public class EFComments : IComments
    {
        private readonly AppDBContent context;

        public EFComments(AppDBContent context)
        {
            this.context = context;
        }
        public IEnumerable<Comment> Allcomments => context.comments;
        public IEnumerable<Comment> comments(Location location)
        {
            return context.comments.Where(c => c.LocationId == location.Id).OrderBy(c=>c.LocationId);
 
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

        public void deleteComment(int id)
        {
            //Пояснення
            //https://qastack.ru/programming/2471433/how-to-delete-an-object-by-id-with-entity-framework
            context.comments.Remove(new Comment() { Id = id });
            context.SaveChanges();
        }
        public void deleteComment(List<Comment> coms)
        {
            foreach(var r in coms)
            {
                context.comments.Remove(r);
            }
            context.SaveChanges();
        }
    }
}
