using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistBase
    {
        public PlaylistBase()
        {

        }
        
        // Fields
        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        [DisplayName("Playlist Name")]
        [Required, StringLength(120)]
        public string Name { get; set; }
    }


    public class PlaylistWithDetails : PlaylistBase
    {
        [DisplayName("Tracks in Playlist")]
        public ICollection<TrackBase> Tracks { get; set; }

        public int TracksCount { get; set; }
    }

    public class PlaylistEditTracksForm
    {
        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        [DisplayName("Playlist Name")]
        [Required, StringLength(120)]
        public string Name { get; set; }
        public int TracksCount { get; set; }
        public MultiSelectList TrackList { get; set; }
        public IEnumerable<TrackBase> TracksNowOnPlaylist { get; set; }
    }

    public class PlaylistEditTracks
    {
        public PlaylistEditTracks()
        {
            TracksIds = new List<int>();
        }


        [Key]
        [DisplayName("Playlist Id")]
        public int PlaylistId { get; set; }

        public IEnumerable<int> TracksIds { get; set; }

    }

}