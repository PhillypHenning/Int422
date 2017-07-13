using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackBase : TrackAdd
    {
        [Key]
        public int TrackId { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {
        [DisplayName("Media Type")]
        public MediaTypeBase MediaType { get; set; }

        [DisplayName("Album title")]
        public String AlbumTitle { get; set; }

        [DisplayName("Artist name")]
        public String AlbumArtistName { get; set; }

    }



    public class TrackAddForm 
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [DisplayName("Length (ms)")]
        public int Milliseconds { get; set; }

        [DisplayName("Unit Price")]
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        public SelectList AlbumList { get; set; }

        public SelectList MediaTypeList { get; set; }
    }

    public class TrackAdd
    {      
        [Required]
        [StringLength(200)]
        [DisplayName("Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [DisplayName("Length (ms)")]
        public int Milliseconds { get; set; }

        [DisplayName("Unit Price")]
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }
        
        public int AlbumId { get; set; } 

        public int MediaTypeId { get; set; }



        public TrackAdd()
        {
            Milliseconds = 0;
            UnitPrice = 0.00m;
        }
    }
}