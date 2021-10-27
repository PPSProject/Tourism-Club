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
                //Якщо у вас помилка, значить вона можливо тут
                return context.comments.Include(c => c.LocationId);
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

        public void deleteComment(int id)
        {
            //Пояснення
            //https://qastack.ru/programming/2471433/how-to-delete-an-object-by-id-with-entity-framework
            context.comments.Remove(new Comment() { Id = id });
            context.SaveChanges();
        }
    }
}
