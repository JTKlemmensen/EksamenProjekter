using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class PriceModel
    {
        public int Alder { get; set; }
        public Vare Vare { get; set; }
        public double Price { get; set; }
    }
}