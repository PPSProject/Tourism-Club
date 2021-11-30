using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tourism_club.Models
{
    public class PageModel
    {
        public Location location { get; set; }
        public Frame frame { get; set; }
        public List<User> users { get; set; }
        public List<Comment> comments { get; set; }
        public string[] pathToPhotos { get; set; }

    }
}
