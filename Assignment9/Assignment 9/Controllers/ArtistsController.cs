using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    // Attention - 09 - Most controllers are protected, nav menu links appear if authenticated
    [Authorize]
    public class ArtistsController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            //var o = m.ArtistGetById(id.GetValueOrDefault());
            var o = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Artists/Create
        //[Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            // Create the "add new" artist form
            var form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAllStrings());

            return View(form);
        }

        // POST: Artists/Create
        [HttpPost, ValidateInput(false)]
        //[Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

        //// GET: Artists/5/AddAlbum
        //[Route("artists/{id}/addalbum")]
        //[Authorize(Roles = "Coordinator")]
        //public ActionResult AddAlbum(int? id)
        //{
        //    // Attempt to get the associated object
        //    var a = m.ArtistGetById(id.GetValueOrDefault());

        //    if (a == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        // Prepare the form
        //        var form = new AlbumAddForm();

        //        // Artist identifier and name
        //        form.ArtistId = a.Id;
        //        form.ArtistName = a.Name;

        //        // Genre list
        //        form.GenreList = new SelectList(m.GenreGetAllStrings());

        //        return View(form);
        //    }
        //}

        //// POST: Artists/5/AddAlbum
        //[Route("artists/{id}/addalbum")]
        //[Authorize(Roles = "Coordinator")]
        //[HttpPost, ValidateInput(false)]
        //public ActionResult AddAlbum(int? id, AlbumAdd newItem)
        //{
        //    // Validate the input
        //    if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.ArtistId)
        //    {
        //        return RedirectToAction("details", "artists", new { id = id });
        //    }

        //    // Process the input
        //    var addedItem = m.AlbumAdd(newItem);

        //    if (addedItem == null)
        //    {
        //        return RedirectToAction("details", "artists", new { id = id });
        //    }
        //    else
        //    {
        //        // Attention - Must redirect to the albums controller
        //        return RedirectToAction("index", "albums");
        //    }
        //}



        /*
        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}