using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class ArtistAddForm
    {
        public ArtistAddForm()
        {
            BirthName = "";
            BirthOrStartDate = DateTime.Now.AddYears(-20);
            Profile = "";
        }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        [DataType(DataType.Url)]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }

        [StringLength(10000)]
        [Display(Name = "Artist portrayal")]
        [DataType(DataType.MultilineText)]
        public string Profile { get; set; }

        // Preparing for rich text
        [StringLength(100000)]
        [DataType(DataType.MultilineText)]
        public string Portrayal { get; set; }

    }

    public class ArtistAdd
    {
        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        [DataType(DataType.Url)]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        // Preparing for rich text
        [StringLength(100000)]
        [DataType(DataType.MultilineText)]
        public string Portrayal { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        public int Id { get; set; }
    }

    public class ArtistWithDetails : ArtistBase
    {
        [Display(Name = "Number of albums")]
        public int AlbumsCount { get; set; }
    }
}
