using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{ 
    public class PlaylistController : Controller
    {
        Manager m = new Manager();

        // GET: Playlist
        public ActionResult Index()
        {


            return View(m.PlaylistGetAll());
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            return View(m.PlaylistGetById(id.GetValueOrDefault()));
        }

        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetById(id.GetValueOrDefault());

            if(o == null)
            {
                return RedirectToAction("Index");
            }
            else
            {


                var form = m.mapper.Map<PlaylistEditTracksForm>(o);
                var selectedValues = o.Tracks.Select(t => t.TrackId);

                form.TrackList = new MultiSelectList
                    //Populates the MultiSelectList with all track items.
                    (items: m.TracksGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                //Send the newly created PlaylistEditForm form 
                form.TracksNowOnPlaylist = o.Tracks.OrderBy(t => t.Name);


                return View(form);
            }                
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracks newItem)
        {
            try
            {
                // Check if model state is valid
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Edit", new { id = newItem.PlaylistId });
                }

                // Check for tampering
                if(id.GetValueOrDefault() !=  newItem.PlaylistId)
                {
                    return RedirectToAction("Index");
                }

                // Envoke manager, attepmt update
                var o = m.PlaylistEdit(newItem);

                if (o == null)
                {
                    // There was a problem updating the object
                    // Our "version 1" approach is to display the "edit form" again
                    return RedirectToAction("Index");
                }
                else
                {
                    // Show the details view, which will have the updated data
                    return RedirectToAction("Details", new { id = newItem.PlaylistId });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlist/Delete/5
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
