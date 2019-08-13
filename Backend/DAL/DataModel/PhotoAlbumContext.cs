using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL.DataModel
{
    public class PhotoAlbumContext : IdentityDbContext<User>
    {
        static PhotoAlbumContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public PhotoAlbumContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
    }
}
