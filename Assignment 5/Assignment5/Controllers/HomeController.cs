﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Dis App.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Phil Henning";

            return View();
        }
    }
}