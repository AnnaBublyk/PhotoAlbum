using DAL.DataModel;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
   
    public class ProfileRepository : IRepository<Profile>
    {
        private PhotoAlbumContext db;

        /// <summary>Initializes a new instance of the <see cref="T:DAL.Repositories.ProfileRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProfileRepository(PhotoAlbumContext context)
        {
            db = context;
        }

        public IQueryable<Profile> GetAll()
        {
            return db.Profiles;
        }

        public Profile GetById(int id)
        {
            var result = db.Profiles.Find(id);
            if (!ReferenceEquals(result, null))
                return result;
            return null;
        }

        public void Create(Profile profile)
        {
            db.Profiles.Add(profile);
        }

        public void Delete(Profile profile)
        {
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }

        public void Update(Profile entity)
        {
            var profile = db.Profiles.First(p => p.ProfileId == entity.ProfileId);
            db.Profiles.Attach(profile);
            if (entity.FirstName != null) profile.FirstName = entity.FirstName;
            if (entity.LastName != null) profile.LastName = entity.LastName;
            profile.IsBlocked = entity.IsBlocked;
          
            db.SaveChanges();
        }

    }
}
