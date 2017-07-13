using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackController : Controller
    {
        private Manager m = new Manager();


        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            //Sends user to Index if id was invalid.
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var o = m.TrackGetById(id);

            return View(o);
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            // God damn this jiffy work
            // Creates a new TrackAdd object, envoking the default constructor for the displaying of default values!
            TrackAddForm trackaddform = new Controllers.TrackAddForm();
            ViewData.Model = trackaddform;

            trackaddform.AlbumList = new SelectList(m.AlbumGetAll().OrderBy(item => item.Title), "AlbumId", "Title");
            trackaddform.MediaTypeList = new SelectList(m.MediaTypeGetAll().OrderBy(item => item.Name), "MediaTypeId", "Name");


            return View(trackaddform);
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newTrack)
        {
            if (!ModelState.IsValid)
            {
                //Returns invalid track for re-entry. 
                return View(newTrack);
            }

            var addedItem = m.TrackAdd(newTrack);

            // TODO: Add insert logic here
            if (addedItem != null)
            {
                return View("Details", m.TrackGetById(addedItem.TrackId));
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track/Edit/5
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

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track/Delete/5
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
    }
}
