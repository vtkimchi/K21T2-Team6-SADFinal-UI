using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Employees
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string Salary { get; set; }
    }
}