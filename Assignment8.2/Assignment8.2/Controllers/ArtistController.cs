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
            return View(m.ArtistGetAllWithDetail());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistWithDetailGetById(id.GetValueOrDefault());

            if (o == null) {
                return HttpNotFound();
            } else { return  View(o); }
        }

        // GET: Artist/Create
        public ActionResult Create()
        {
            var form = new ArtistAddForm();

            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            
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
        // Get: Add album
        [Route("Artist/{id}/addartist")]
        public ActionResult AddAlbum(int? id)
        {
            var a = m.ArtistWithDetailGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }else
            {
                var o = new AlbumAddForm();
                o.ArtistId = a.Id;
                o.ArtistName = a.Name;
                o.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                o.ArtistList = new MultiSelectList
                    (items: m.ArtistGetAll(),
                     dataValueField: "Id",
                     dataTextField: "Name",
                     selectedValues: new List<int>() { id.GetValueOrDefault() }
                     );
                o.TrackList = new MultiSelectList
                    (items: m.TrackGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: null);

                return View(o);
            }
        }

        // POST: Add album
        [Route("Artist/{id}/addartist")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAdd newItem)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process
            var addedItem = m.AlbumAdd(newItem);

            if(addedItem == null)
            {
                return View(newItem);
            }else
            {
                return RedirectToAction("details", "album", new { id = addedItem.Id });
            }
        }
    }
}
