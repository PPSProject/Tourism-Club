using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourism_club.Models
{
    public class Frame
    {
        public int Id { get; set; }
        public string source { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string style { get; set; }
        public string screen { get; set; }
        public string loading { get; set; }
        public int LocationId { get; set; }
    }
}
