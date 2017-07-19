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
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }

    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthOrStartDate = DateTime.Now;
            UrlArtist = "https://p6.zdassets.com/hc/theme_assets/808026/200152607/icon.reddit.gray.svg";
        }

        [Required, StringLength(200)]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Artist cover URL")]
        public string UrlArtist { get; set; }

        [StringLength(200)]
        public string Executive { get; set; }
        [Display(Name = "Birth or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [StringLength(200)]
        [Display(Name = "Birth name (if applicable)")]
        public string BirthName { get; set; }
        public IEnumerable<int> AlbumIds { get; set; }
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