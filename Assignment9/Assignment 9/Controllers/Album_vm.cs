using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class AlbumAddForm
    {
        public AlbumAddForm()
        {
            ReleaseDate = DateTime.Now;
        }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "URL to album photo")]
        [DataType(DataType.Url)]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album primary genre")]
        public SelectList GenreList { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Coordinator")]
        public string Coordinator { get; set; }

        // Preparing for rich text
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Depiction { get; set; }
    }

    public class AlbumAdd
    {
        public AlbumAdd()
        {

        }
        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "URL to album photo")]
        [DataType(DataType.Url)]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album primary genre")]
        public string Genre { get; set; }

        // Preparing for rich text
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Depiction { get; set; }

    }

    public class AlbumBase : AlbumAdd{
        public int Id { get; set; }    
    } 

    public class AlbumWithDetails : AlbumBase
    {
        [Display(Name = "Number of tracks")]
        public int TracksCount { get; set; }
    }
}