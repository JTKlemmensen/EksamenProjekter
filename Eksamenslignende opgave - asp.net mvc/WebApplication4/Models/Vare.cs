using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Vare
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public override String ToString()
        {
            return Name;
        }
    }
}