using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitement.Models
{
    public class Candidates
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Phone { get; set; }
    }
}