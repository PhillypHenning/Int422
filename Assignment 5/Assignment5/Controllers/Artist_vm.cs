using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class ArtistBase
    {
        public ArtistBase() { }

        //Fields
        [Key]
        public int ArtistId;

        [Required]
        [StringLength(255)]
        public String Name;
    }
}