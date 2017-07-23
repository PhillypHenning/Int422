using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8._2.Models;
using System.Security.Claims;

namespace Assignment8._2.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                // Album Use Cases
                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();
                cfg.CreateMap<Models.Album, Controllers.AlbumWithDetail>();
                cfg.CreateMap<Controllers.AlbumAdd, Models.Album>();

                // Artist Use Cases
                cfg.CreateMap<Models.Artist, Controllers.ArtistBase>();
                cfg.CreateMap<Models.Artist, Controllers.ArtistWithDetail>();
                cfg.CreateMap<Controllers.ArtistAdd, Models.Artist>();

                // Tracks Use Case
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();
                cfg.CreateMap<Models.Track, Controllers.TrackWithDetail>();
                cfg.CreateMap<Controllers.TrackAdd, Models.Track>();
                cfg.CreateMap<Controllers.TrackBase, Controllers.TrackEditInfoForm>();

                // Genre Use Cases
                cfg.CreateMap<Models.Genre, Controllers.GenreBase>();              
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }
        
        // ########### Album ###########################
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums);
        }

        public IEnumerable<AlbumWithDetail> AlbumGetAllWithDetail()
        {
            var c = ds.Albums.Include("Artists").Include("Tracks");
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumWithDetail>>(c);
        }

        public AlbumBase AlbumGetById(int? id)
        {
            var o = ds.Albums.Find(id.GetValueOrDefault());
            return mapper.Map<Album, AlbumBase>(o);
        }

        public AlbumWithDetail AlbumWithDetailGetById(int? id)
        {
            var o = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(m => m.Id == id);
            return mapper.Map<Album, AlbumWithDetail>(o);
        }

        public AlbumWithDetail AlbumAdd(AlbumAdd newItem)
        {
            var a = ds.Artists.Include("Albums").FirstOrDefault(ar => ar.Id == newItem.ArtistId);
            if (a ==null)
            {
                return null;
            }else
            {
                foreach (var item in newItem.TrackIds)
                {
                    var b = ds.Tracks.Find(item);
                    ds.Tracks.Add(b);
                }

                var addedItem = ds.Albums.Add(mapper.Map<AlbumAdd, Album>(newItem));
                addedItem.Artists.Add(a);
                //addedItem.Tracks.Add(b);
                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Album, AlbumWithDetail>(addedItem);
            }


        }

        // ########### Artist ###########################
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists);
        }

        public IEnumerable<ArtistWithDetail> ArtistGetAllWithDetail()
        {
            var c = ds.Artists.Include("Albums");
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistWithDetail>>(c);
        }

        public ArtistBase ArtistGetById(int? id)
        {
            var o = ds.Artists.Find(id.GetValueOrDefault());
            return mapper.Map<Artist, ArtistBase>(o);
        }

        public ArtistWithDetail ArtistWithDetailGetById(int? id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(m => m.Id == id);
            return mapper.Map<Artist, ArtistWithDetail>(o);
        }

        public ArtistWithDetail ArtistAdd(ArtistAdd newItem)
        {
            var a = ds.Albums.Find(newItem.AlbumIds);

            var addedItem = ds.Artists.Add(mapper.Map<ArtistAdd, Artist>(newItem));

            if (a != null)
            {
                addedItem.Albums.Add(a);
            }

            ds.SaveChanges();

            return (addedItem == null) ? null : mapper.Map<Artist, ArtistWithDetail>(addedItem);
        }

        // ########### Track ###########################
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllWithDetail()
        {
            var c = ds.Tracks.Include("Albums.Artists");
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetail>>(c);
        }

        public TrackBase TrackGetById(int? id)
        {
            var o = ds.Tracks.Find(id.GetValueOrDefault());
            return mapper.Map<Track, TrackBase>(o);
        }

        public TrackWithDetail TrackWithDetailGetById(int? id)
        {
            var o = ds.Tracks.Include("Albums.Artists").SingleOrDefault(m => m.Id == id);

            if (o == null)
            {
                return null;
            }
            else
            {
                // Create the result collection
                var result = mapper.Map<Track, TrackWithDetail>(o);
                return result;
            }
        }

        public TrackWithDetail TrackAdd(TrackAdd newItem)
        {
            var a = ds.Albums.Find(newItem.AlbumIds);

            if (a == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(newItem));
                addedItem.Albums.Add(a);
                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetail>(addedItem);
            }
        }

        public TrackBase TrackEditInfo(TrackEditInfo editItem)
        {
            var o = ds.Tracks.FirstOrDefault(t => t.Id == editItem.Id);

            if(o == null)
            {
                return null;
            }else
            {
                editItem.Clerk = o.Clerk;
                ds.Entry(o).CurrentValues.SetValues(editItem);
                ds.SaveChanges();
            }
            return mapper.Map<Track, TrackBase>(o);
        }

        public bool TrackDelete(int? id)
        {
            var itemToDel = ds.Tracks.Find(id.GetValueOrDefault());

            if (itemToDel == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(itemToDel);
                ds.SaveChanges();
                return true;
            }
        }


        // ########### Genre ###########################
        public IEnumerable<GenreBase> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(m=> m.Name));
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






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            var mail = "@example.com";
            var exec = "exec" + mail;
            var coord = "coord" + mail;
            var clerk = "clerk" + mail;

            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

            }
            if (ds.Genres.Count() == 0)
            {
                ds.Genres.Add(new Genre { Name = "Indie"});
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Chill" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Hip Hop" });
                ds.Genres.Add(new Genre { Name = "RnB" });
                ds.Genres.Add(new Genre { Name = "Country" });    
            }
            if (ds.Artists.Count() == 0)
            {
                ds.Artists.Add(new Artist()
                {
                    BirthName = "Billy Washington",
                    BirthOrStartDate = new DateTime(2010, 1, 1),
                    Executive = exec,
                    Genre = "Rock",
                    Name = "I set the sea on fire",
                    UrlArtist = "http://f4.bcbits.com/img/0005375454_100.png"
                });
                ds.Artists.Add(new Artist()
                {
                    BirthName = "Matthew Shultz",
                    BirthOrStartDate = new DateTime(2006, 1, 1),
                    Executive = exec,
                    Genre = "Rock",
                    Name = "Cage the elephant",
                    UrlArtist = "https://images.genius.com/f9f7668a347f97eb18a666a7f79faaa7.820x510x1.jpg"
                });
                ds.Artists.Add(new Artist()
                {
                    BirthName = "I don't listen to pop",
                    BirthOrStartDate = new DateTime(2000, 1, 1),
                    Executive = exec,
                    Genre = "Pop",
                    Name = "PopSucks!",
                    UrlArtist = "http://kennethwoods.net/blog1/wp-content/uploads/2015/07/pop-sucks.jpg"
                });
                ds.Artists.Add(new Artist()
                {
                    BirthName = "Zach Condon",
                    BirthOrStartDate = new DateTime(2006, 1, 1),
                    Executive = exec,
                    Genre = "Indie",
                    Name = "Beirut",
                    UrlArtist = "http://beirutband.com/wp-content/uploads/2015/09/video-elephantgun.jpg"
                });

                ds.SaveChanges();
            }
            if (ds.Albums.Count() == 0)
            {
                var CTE = ds.Artists.SingleOrDefault(a => a.Name == "Cage the Elephant");
                var Beirut = ds.Artists.SingleOrDefault(a => a.Name == "Beirut");
                var ISTSOF = ds.Artists.SingleOrDefault(a => a.Name == "I set the sea on fire");


                ds.Albums.Add(new Album()
                {
                    Artists = new List<Artist> { CTE },
                    Coordinator = coord,
                    Genre = "Rock",
                    Name = "Come a little a closer",
                    ReleaseDate = new DateTime(2009, 12, 23),
                    UrlAlbum = "https://images.rapgenius.com/82a9425606af34c5daa3309a4eb7eea7.1000x1000x1.jpg"
                });
                ds.Albums.Add(new Album()
                {
                    Artists = new List<Artist> { CTE },
                    Coordinator = coord,
                    Genre = "Rock",
                    Name = "Ain't no rest for wicked",
                    ReleaseDate = new DateTime(2009, 12, 23),
                    UrlAlbum = "https://images.rapgenius.com/82a9425606af34c5daa3309a4eb7eea7.1000x1000x1.jpg"
                });
                ds.Albums.Add(new Album()
                {
                    Artists = new List<Artist> { Beirut },
                    Coordinator = coord,
                    Genre = "Indie",
                    Name = "Elephant Gun",
                    ReleaseDate = new DateTime(2009, 12, 23),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/b/b0/Elephant_Gun_%28EP%29_%28Front_Cover%29.png"
                });
                ds.Albums.Add(new Album()
                {
                    Artists = new List<Artist> { Beirut },
                    Coordinator = coord,
                    Genre = "Indie",
                    Name = "Nantes",
                    ReleaseDate = new DateTime(2009, 12, 23),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/b/b0/Elephant_Gun_%28EP%29_%28Front_Cover%29.png"
                });
                ds.Albums.Add(new Album()
                {
                    Artists = new List<Artist> { ISTSOF },
                    Coordinator = coord,
                    Genre = "Rock",
                    Name = "Tastes like funk",
                    ReleaseDate = new DateTime(2011, 12, 23),
                    UrlAlbum = "http://glasswerk.co.uk/img/news/tastes%20like%20funk%20packshot_1450190139.jpg"
                });

                ds.SaveChanges();
            }

            var TLF = ds.Albums.SingleOrDefault(a => a.Name == "Tastes like funk");
            var EG = ds.Albums.SingleOrDefault(a => a.Name == "Elephant Gun");
            var ANRFTW = ds.Albums.SingleOrDefault(a => a.Name == "Ain't no rest for wicked");


            if (ds.Tracks.Count() == 0)
            {
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "Sonny John Moore",
                    Genre = "Rock",
                    Name = "Right In",
                    Albums = new List<Album> { TLF }
                });
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "Sonny John Moore",
                    Genre = "Rock",
                    Name = "Right Out",
                    Albums = new List<Album> { TLF }
                });
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "Sonny John Moore",
                    Genre = "Rock",
                    Name = "Right Over the top",
                    Albums = new List<Album> { TLF }
                });

                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "This guy",
                    Genre = "Indie",
                    Name = "Right In",
                    Albums = new List<Album> { EG }
                });
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "This guy",
                    Genre = "Indie",
                    Name = "Not Right In",
                    Albums = new List<Album> { EG }
                });
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "This guy",
                    Genre = "Indie",
                    Name = "I'm becoming very tired of writing these",
                    Albums = new List<Album> { EG }
                });

                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "Phil not Henning",
                    Genre = "Rock",
                    Name = "In one ear",
                    Albums = new List<Album> { ANRFTW }
                });
                ds.Tracks.Add(new Track()
                {
                    Clerk = clerk,
                    Composer = "Phil not Henning",
                    Genre = "Rock",
                    Name = "Shake me down",
                    Albums = new List<Album> { ANRFTW }
                });
            }

            ds.SaveChanges();
            done = true;

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}