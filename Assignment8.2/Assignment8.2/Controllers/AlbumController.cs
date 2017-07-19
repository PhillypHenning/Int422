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
            return View(m.AlbumGetAllWithDetail());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumWithDetailGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }else
            {
                return View(o);
            }
        }

        // GET: Album/Create
        // Find a way to redirect to the Artist AddAlbum function. 
        public ActionResult Create()
        {
            var form = new AlbumAddForm();

            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

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

        [Route("Album/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var a = m.AlbumWithDetailGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                var o = new TrackAddForm();
                o.AlbumId = a.Id;
                o.AlbumName = a.Name;
                o.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                o.AlbumList = new MultiSelectList(
                    items: m.AlbumGetAll(),
                    dataValueField: "Id",
                      dataTextField: "Name",
                      selectedValues: new List<int>() { id.GetValueOrDefault() }
                    );

                return View(o);
            }
        }

        [Route("Album/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", "track", new { id = addedItem.Id });
            }
        }
    }
}
