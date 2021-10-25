using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tourism_club.Models
{
    public class Comments:Location
    {
        [Key]
        public int CommentId { get; set; }
        public string comment { get; set; }
        public int LocationId { get; set; }
    }

}
