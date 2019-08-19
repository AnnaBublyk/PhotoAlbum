using DAL.DataModel;
using DAL.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class LikeRepository:IRepository<Like>
    {
        private PhotoAlbumContext db;

        /// <summary>Initializes a new instance of the <see cref="T:DAL.Repositories.LikeRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public LikeRepository(PhotoAlbumContext context)
        {
            db = context;
        }

        public IQueryable<Like> GetAll()
        {
            return db.Likes;
        }

        public Like GetById(int id)
        {
            var result = db.Likes.Find(id);
            if (!ReferenceEquals(result, null))
                return result;
            return null;
        }

        public void Create(Like like)
        {
            db.Likes.Add(like);
            db.SaveChanges();
        }

        public void Delete(Like like)
        {
            var y = (from x in db.Likes where (x.PhotoId == like.PhotoId && x.ProfileId==like.ProfileId) select x).First();
            db.Likes.Remove(y);
            db.SaveChanges();
        }

        public void Update(Like like)
        {
            db.Entry(like).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
