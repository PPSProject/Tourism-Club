using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tourism_club.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string comment { get; set; }
        
        public int LocationId { get; set; }
        public string CommentatorId { get; set; }

        public virtual Location Location { get; set; }
    }

}
