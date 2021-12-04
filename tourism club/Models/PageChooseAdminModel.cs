using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourism_club.Models
{
    public class PageChooseAdminModel
    {
        public IEnumerable<Location> locations { get; set; }
        public List<User> users { get; set; }
    }
}
