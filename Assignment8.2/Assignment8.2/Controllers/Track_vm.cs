using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class TrackAddForm : TrackAdd
    {
        [StringLength(200)]
        public SelectList GenreList { get; set; }
        public string GenreName { get; set; }
    }

    public class TrackAdd
    {

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Composer { get; set; }

        [Required, StringLength(200)]
        public string Clerk { get; set; }

        public int AlbumId { get; set; }
    }

    public class TrackBase : TrackAdd
    {
        public int Id { get; set; }
    }

    public class TrackWithDetail : TrackBase {
        TrackWithDetail()
        {
            Albums = new List<AlbumBase>();
        }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }
}