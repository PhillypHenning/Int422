using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class GenreController : Controller
    {
        private Manager m = new Manager();

        // GET: Genre
        public ActionResult Index()
        {
            return View(m.GenreGetAll());
        }
    }
}
