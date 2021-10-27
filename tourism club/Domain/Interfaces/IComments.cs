using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domains.Interfaces
{
    interface IComments
    {
        IEnumerable<Comment> comments { get; }
        void addComment(Comment comm);

    }
}
