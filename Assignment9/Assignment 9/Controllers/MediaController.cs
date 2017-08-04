using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class MediaController : Controller
    {
        Manager m = new Manager();

        // GET: Media
        public ActionResult Index()
        {
            return View();
        }


        [Route("clip/{id}")]
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.MediaGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Attention - 9 - Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.Media, o.MediaContentType);
            }
        }
    }
}