using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    public interface IRoles
    {
        Role getRole(User user);
        void addRole(Role role);
    }
}
