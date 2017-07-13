using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class AlbumController : Controller
    {
        private Manager m = new Manager();

        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }else
            {
                return View(o);
            }
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            var form = new AlbumAddForm();

            form.GenreList = new SelectList(m.GenreGetAll());

            return View(form);
        }

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(AlbumAdd newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }

                var addedItem = m.AlbumAdd(newItem);

                if(addedItem == null)
                {
                    return HttpNotFound();
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

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
