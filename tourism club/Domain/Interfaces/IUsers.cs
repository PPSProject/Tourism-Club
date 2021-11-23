using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    public interface IUsers
    {
        IEnumerable<User> users { get; }
        User getUser(int id);
        void addUser(User user);
        void removeUser(int id);
    }
}
