using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    public interface IComments
    {
        IEnumerable<Comment> Allcomments { get; }
        IEnumerable<Comment> comments(Location location);
        void addComment(Comment comm);
        void deleteComment(int id);

        void deleteComment(List<Comment> coms);
    }
}
