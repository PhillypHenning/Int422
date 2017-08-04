using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class TrackController : Controller
    {
        Manager m = new Manager();
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            return View(m.TrackGetIdWithDetails(id.GetValueOrDefault()));
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            var form = new TrackAddForm();
            form.GenreList = new SelectList(m.GenreGetAllStrings());

            return View(form);
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.TrackGetIdWithDetails(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<TrackWithDetails, TrackEditInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditInfo newItem)
        {
            if (!ModelState.IsValid) { 
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {

                return RedirectToAction("index");
            }

            var editedItem = m.TrackEditInfo(newItem);

            if (editedItem == null)
            {

                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {

                return RedirectToAction("details", new { id = newItem.Id });
            }
        }

        public ActionResult Delete(int? id)
        {
            var trackToDelete = m.TrackGetIdWithDetails(id.GetValueOrDefault());

            if(trackToDelete == null)
            {
                return RedirectToAction("index");
            }else
            {
                return View(trackToDelete);
            }
        }

        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());

            return RedirectToAction("index");
        }

    }
}
