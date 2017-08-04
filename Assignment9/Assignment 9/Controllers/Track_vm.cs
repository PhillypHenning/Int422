using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class TrackAdd
    {
        public TrackAdd()
        { }
        [Required, StringLength(200)]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        public string Composers { get; set; }

        [Required]
        public string Genre { get; set; }

        // User name who added/edited the track
        [StringLength(200)]
        public string Clerk { get; set; }

        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }

    public class TrackAddForm
    {
        public TrackAddForm()
        {
        }
        [Required, StringLength(200)]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        public string Composers { get; set; }

        // User name who added/edited the track
        [StringLength(200)]
        public string Clerk { get; set; }
        [Required]
        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }

    }

    public class TrackBase : TrackAdd
    {
        public int Id { get; set; }
    }

    public class TrackWithDetails : TrackBase
    {
        [Display(Name = "Number of albums")]
        public int AlbumsCount { get; set; }
    }

    public class TrackMedia
    {
        public int Id { get; set; }
        public string MediaContentType { get; set; }
        public byte[] Media { get; set; }
    }

    public class TrackEditInfoForm
    {
        TrackEditInfoForm() { }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }
    }

    public class TrackEditInfo
    {
        public TrackEditInfo() { }

        [Key]
        public int Id { get; set; }

        public HttpPostedFileBase MediaUpload { get; set; }
    }
}