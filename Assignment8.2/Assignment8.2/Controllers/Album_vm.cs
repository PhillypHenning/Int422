using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class AlbumAddForm
    {
        [StringLength(200)]
        public SelectList GenreList { get; set; }
        public string GenreName { get; set; }
    }

    public class AlbumAdd
    {
        public AlbumAdd()
        {
            ReleaseDate = DateTime.Now;
        }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Coordinator { get; set; }
    
        [StringLength(200)]
        public string UrlAlbum { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ArtistId { get; set; }
        public int TrackId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {
        AlbumWithDetail()
        {
            Artists = new List<ArtistBase>();
            Tracks = new List<TrackBase>();
        }

        public IEnumerable<TrackBase> Tracks { get; set; }
        public IEnumerable<ArtistBase> Artists { get; set; }
    }
}