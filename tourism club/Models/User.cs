using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace tourism_club.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string mail { get; set; }
        public Role existadminrole { get; set; }
    }
}
