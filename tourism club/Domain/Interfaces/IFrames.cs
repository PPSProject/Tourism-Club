using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    public interface IFrames
    {
        Frame getFrame(Location location);
        void addFrame(Frame frame);
        void removeFrame(Location location);
    }
}
