using DAL.DataModel;
using DAL.Interfaces;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private PhotoAlbumContext db;

        /// <summary>Initializes a new instance of the <see cref="T:DAL.Repositories.TagRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public TagRepository(PhotoAlbumContext context)
        {
            db = context;
        }

        public IQueryable<Tag> GetAll()
        {
            return db.Tags;
        }

        public Tag GetById(int id)
        {
            var result = db.Tags.Find(id);
            if (!ReferenceEquals(result, null))
            {
                return result;
            }
            return null;
        }

        public void Create(Tag tag)
        {
            if (tag.Name == "")
            {
                throw new Exception();
            }

            db.Tags.Add(tag);
            db.SaveChanges();
            
        }

        public void Delete(Tag tag)
        {
            db.Tags.Remove(tag);
            db.SaveChanges();
        }

        public void Update(Tag tag)
        {
            db.Tags.AddOrUpdate(tag);
            db.SaveChanges();
        }
    }
}
