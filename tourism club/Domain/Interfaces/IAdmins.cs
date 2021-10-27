using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain.Interfaces
{
    interface IAdmins
    {
        IEnumerable<Admin> admins { get; }
        Admin getAdmin(int id);
        void addAdmin(Admin admin);
        void removeAdmin(int id);
    }
}
