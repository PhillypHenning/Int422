using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8._2.Controllers
{
    public class AlbumAddForm : AlbumAdd
    {
        [StringLength(200)]
        public SelectList GenreList { get; set; }
        public string GenreName { get; set; }

        public MultiSelectList ArtistList { get; set; }
        public string ArtistName { get; set; }

        public MultiSelectList TrackList { get; set; }
    }

    public class AlbumAdd
    {
        public AlbumAdd()
        {
            ReleaseDate = DateTime.Now;
            TrackIds = new List<int>();
            UrlAlbum = "https://p6.zdassets.com/hc/theme_assets/808026/200152607/icon.reddit.gray.svg";
        }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Coordinator { get; set; }
    
        [StringLength(200)]
        public string UrlAlbum { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public IEnumerable<int> TrackIds { get; set; }
        public string primaryGenre { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public AlbumBase()
        {
            ArtistIds = new List<int>();
        }

        [Key]
        public int Id { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {
        AlbumWithDetail()
        {
            Artists = new List<ArtistBase>();
            Tracks = new List<TrackBase>();
        }
        public int TrackCount { get; set; }

        public IEnumerable<TrackBase> Tracks { get; set; }
        public IEnumerable<ArtistBase> Artists { get; set; }
    }
}