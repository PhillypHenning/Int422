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
            return View(m.TrackGetAllWithDetail());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            return View(m.TrackWithDetailGetById(id.GetValueOrDefault()));
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
        public ActionResult Edit(int? id)
        {
            var o = m.TrackGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<TrackBase, TrackEditInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditInfo editItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = editItem.Id });
            }

            if(id.GetValueOrDefault() != editItem.Id)
            {
                RedirectToAction("Index");
            }

            var editedItem = m.TrackEditInfo(editItem);
            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = editItem.Id });
            }
            else{
                return RedirectToAction("Details", new { id = editItem.Id });
            }
        }

        // GET: Track/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDel = m.TrackGetById(id.GetValueOrDefault());

            if(itemToDel == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemToDel);
            }
        }

        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                var result = m.TrackDelete(id.GetValueOrDefault());               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
