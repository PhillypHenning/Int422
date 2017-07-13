using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class ArtistAddForm : ArtistAdd
    {
        [StringLength(200)]
        public SelectList GenreList { get; set; }
        public string GenreName { get; set; }
    }

    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthOrStartDate = DateTime.Now;
        }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string UrlArtist { get; set; }

        [StringLength(200)]
        public string Executive { get; set; }

        public DateTime BirthOrStartDate { get; set; }

        [StringLength(200)]
        public string BirthName { get; set; }
        public int AlbumId { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class ArtistWithDetail : ArtistBase
    {
        ArtistWithDetail()
        {
            Albums = new List<AlbumBase>();
        }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }
}