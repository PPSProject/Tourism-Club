using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tourism_club.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string LocationTitle { get; set; }
        public string LocationDescription { get; set;}
        public string PathToPhotos { get; set; }
        public List<Comment> comments { get; set; }
    }
}
