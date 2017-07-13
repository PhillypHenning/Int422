using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                //Album
                cfg.CreateMap<Album, AlbumBase>();

                //Artist
                cfg.CreateMap<Artist, ArtistBase>();


                //MediaType 
                cfg.CreateMap<MediaType, MediaTypeBase>();


                //Track
                cfg.CreateMap<Track, TrackBase>();
                cfg.CreateMap<Track, TrackWithDetail>();
                cfg.CreateMap<TrackAdd, Track>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()


        //Album 
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(item => item.AlbumId));
        }

        public AlbumBase AlbumGetById(int? id)
        {
            var o = ds.Albums.Find(id.GetValueOrDefault());

            return mapper.Map<Album, AlbumBase>(o);
        }

        //Artist
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(item => item.ArtistId));
        }
        public ArtistBase ArtistGetById(int? id)
        {
            var o = ds.Artists.Find(id.GetValueOrDefault());

            return mapper.Map<Artist, ArtistBase>(o);
        }


        //MediaType
        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes.OrderBy(item => item.MediaTypeId));
        }

        public MediaTypeBase MediaTypeGetById(int? id) {
            var o = ds.MediaTypes.Find(id.GetValueOrDefault());

            return mapper.Map<MediaType, MediaTypeBase>(o);
        }


        //Tracks
        public IEnumerable<TrackWithDetail> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetail>>(ds.Tracks.Include("Album.Artist").Include("MediaType").OrderBy(item => item.Album.Artist.Name));
        }
        
        public TrackWithDetail TrackGetById(int? id)
        {
            var o = ds.Tracks.Include("Album.Artist").Include("MediaType").Where(item => item.TrackId == id).Single();

            return mapper.Map<Track, TrackWithDetail>(o);
        }

        public TrackBase TrackAdd(TrackAdd newTrack)
        {
            // Validate the AlbumId and MediaTypeId
            //if (ds.Albums.Find(newTrack.AlbumId) == null){ return null; }
            var o = ds.MediaTypes.SingleOrDefault(item => item.MediaTypeId == newTrack.MediaTypeId);
            var p = ds.Albums.SingleOrDefault(item => item.AlbumId == newTrack.AlbumId);

            if (o == null || p == null) { return null; }


            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(newTrack));

            // Directly modifying the Track database object (Track.cs)           
            addedTrack.Album = p; addedTrack.MediaType = o;
            
            // The association is saved, the Album that is assocatied with the Track.Album is saved.   
            ds.SaveChanges();

            return (addedTrack == null) ? null : mapper.Map<Track, TrackBase>(addedTrack);
                        
        }
    }
}