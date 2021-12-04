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

        public Frame getFrame(Location location)
        {
               // return (Frame)context.frames.Include(c => (int)c.LocationId == location.Id);
            return (Frame)context.frames.FirstOrDefault(x => x.LocationId == location.Id);
        }

        public void removeFrame(Location location)
        {
            Frame frame = (Frame)context.frames.FirstOrDefault(x => x.LocationId == location.Id);
            context.frames.Remove(frame);
            context.SaveChanges();
        }
    }
}
