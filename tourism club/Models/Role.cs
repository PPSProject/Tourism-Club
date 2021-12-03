using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tourism_club.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool adminRole { get; set; }
    }
}
