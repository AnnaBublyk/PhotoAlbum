using DAL.DataModel;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private PhotoAlbumContext db;
        private PhotoRepository photoRepository;
        private ProfileRepository profileRepository;
        private TagRepository tagRepository;
        private LikeRepository likeRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;


        /// <summary>Initializes a new instance of the <see cref="T:DAL.Repositories.UnitOfWork"/> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        public UnitOfWork(string connectionString)
        {
            db = new PhotoAlbumContext(connectionString);
        }
        public IPhotoRepository Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = new PhotoRepository(db);
                return photoRepository;
            }
        }
        public IRepository<Like> Likes
        {
            get
            {
                if (likeRepository == null)
                    likeRepository = new LikeRepository(db);
                return likeRepository;
            }
        }

        public IRepository<Profile> Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = new ProfileRepository(db);
                return profileRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (tagRepository == null)
                    tagRepository = new TagRepository(db);
                return tagRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<User>(db));

                return userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<Role>(db));
                return roleManager;
            }
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        public void Save()
        {
             db.SaveChanges();
        }
    }
}
