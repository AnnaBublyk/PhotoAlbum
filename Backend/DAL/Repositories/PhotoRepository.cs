using DAL.DataModel;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private PhotoAlbumContext db;

        /// <summary>Initializes a new instance of the <see cref="T:DAL.Repositories.PhotoRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public PhotoRepository(PhotoAlbumContext context)
        {
            db = context;
        }

        public IQueryable<Photo> GetAll()
        {
            return db.Photos;
            
        }

        public Photo GetById(int id)
        {
            var result = db.Photos.Find(id);
            if (!ReferenceEquals(result, null))
                return result;
            return null;
        }
        public void Create(Photo photo)
        {
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public void Delete(Photo photo)
        {
            var y = (from x in db.Photos where x.PhotoId == photo.PhotoId select x).First();
            new List<string>(Directory.GetFiles(@"D:\study\Epam\PhotoAlbum\PL\Data\")).ForEach(file => { if (file.Contains(y.PictureName)) File.Delete(file); });
            db.Photos.Remove(y);
            db.SaveChanges();
        }

        public void Update(Photo photo)
        {
            var dbEntity =  db.Set<Photo>().SingleOrDefault(x => x.PhotoId == photo.PhotoId);
            var tags = photo.Tags.ToList();
            photo.Tags.Clear();
            foreach (var tag in tags)
            {
                var dbTag = db.Tags.FirstOrDefault(t => t.Name == tag.Name);
                db.Tags.Attach(dbTag);
                dbTag.Photos.Add(dbEntity);
                db.SaveChanges();
            }

            dbEntity.Tags = tags;
            db.SaveChanges();
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
    }

