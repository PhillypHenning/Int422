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
        public MultiSelectList AlbumList { get; set; }
        [StringLength(200)]
        public string AlbumName { get; internal set; }

        public SelectList GenreList { get; set; }
        [StringLength(200)]
        public string GenreName { get; set; }
    }

    public class TrackAdd
    {
        public TrackAdd()
        {
            AlbumIds = new List<int>();
        }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public int AlbumId { get; internal set; }

        public string Composer { get; set; }
        public string Genre { get; set; }

        [Required, StringLength(200)]
        public string Clerk { get; set; }

        public IEnumerable<int> AlbumIds { get; set; }
    }

    public class TrackBase : TrackAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class TrackWithDetail : TrackBase {
        TrackWithDetail()
        {
            Albums = new List<AlbumBase>();
            
        }

        public IEnumerable<string> AlbumNames { get; internal set; }
        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class TrackEditInfoForm :TrackAddForm
    {
        [Key]
        public int Id { get; set; }
    }

    public class TrackEditInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Clerk { get; set; }
    }
}