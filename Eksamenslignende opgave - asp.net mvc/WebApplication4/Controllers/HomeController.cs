using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View(GetAllVarer());
        }
        
        public ActionResult Price(int? alder, string ret)
        {
            if(alder != null)
            {
                double rabatModifier = 1;
                if (alder < 12)
                    rabatModifier = 0.5;
                else if (alder >= 65)
                    rabatModifier = 0.8;

                foreach (Vare v in GetAllVarer())
                    if (v.Name.ToLower() == ret.ToLower())
                        return View("Price", new PriceModel() { Alder = alder.Value, Price = rabatModifier * v.Price, Vare = v });
            }
            return RedirectToAction("Index");
        }

        private Vare[] GetAllVarer()
        {
            Vare[] v = new Vare[3];
            v[0] = new Vare { Name = "Pasta", Price = 90 };
            v[1] = new Vare { Name = "Pizza", Price = 70 };
            v[2] = new Vare { Name = "Ris", Price = 50 };
            return v;
        }
    }
}