using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
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
            return View(m.TrackGetById(id.GetValueOrDefault()));
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            var form = new TrackAddForm();

            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Id");


            return View(form);
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(TrackAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }
                // TODO: Add insert logic here
                var addedItem = m.TrackAdd(newItem);

                if(addedItem == null)
                {
                    return View(newItem);
                }else
                {
                    return RedirectToAction("details", new { id = addedItem.Id });
                }
            }
            catch
            {
                return View();
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
