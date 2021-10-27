using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tourism_club.Models;
using tourism_club.Domain.Interfaces;

namespace tourism_club.Domain.Classes
{
    public class EFFrames : IFrames
    {
        private readonly AppDBContent context;

        public EFFrames(AppDBContent context)
        {
            this.context = context;
        }
        public void addFrame(Frame frame)
        {
            if(frame.Id == default)
            {
                context.Entry(frame).State = EntityState.Added;
            }
            else
            {
                context.Entry(frame).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Frame getFrame
        {
            get
            {
                return (Frame)context.frames.Include(c => c.LocationId);
            }
        }

        public void removeFrame(int id)
        {
            context.frames.Remove(new Frame() { Id = id });
            context.SaveChanges();
        }
    }
}
