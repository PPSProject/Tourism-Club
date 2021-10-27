using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Domain
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Frame> frames { get; set; }
        public DbSet<Gid> gids { get; set; }
    }
}
