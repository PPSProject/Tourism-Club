using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    interface IFrames
    {
        Frame getFrame { get; }
        void addFrame(Frame frame);
        void removeFrame(int id);
    }
}
