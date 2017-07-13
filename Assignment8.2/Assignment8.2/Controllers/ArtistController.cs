using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class ArtistController : Controller
    {
        private Manager m = new Manager();

        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null) {
                return HttpNotFound();
            } else { return  View(o); }
        }

        // GET: Artist/Create
        public ActionResult Create()
        {
            var form = new ArtistAddForm();

            form.GenreList = new SelectList(m.GenreGetAll());
            
            return View(form);
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(ArtistAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }
                
                var addedItem = m.ArtistAdd(newItem);

                if (addedItem == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return RedirectToAction("details", new { id = addedItem.Id });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
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

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
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
