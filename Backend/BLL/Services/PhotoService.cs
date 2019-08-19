using BLL.DTO;
using BLL.Interface;
using DAL.DataModel;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;


namespace BLL.Services
{
    /// <summary>Class PhotoService.
    /// Implements the <see cref="BLL.Interface.IPhotoService"/></summary>
    /// <seealso cref="BLL.Interface.IPhotoService" />
    public class PhotoService:IPhotoService
    {
        IUnitOfWork Database { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:BLL.Services.PhotoService"/> class.</summary>
        /// <param name="uow">The uow.</param>
        public PhotoService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>Gets all.</summary>
        /// <returns>IQueryable&lt;PhotoDTO&gt;.</returns>
        public IQueryable<PhotoDTO> GetAll()
        {
            try
            {
                var photos = from b in Database.Photos.GetAll()
                             select new PhotoDTO()
                             {
                                 ProfileId = b.ProfileId,
                                 UserName=b.UserName,
                                 Likes = b.Likes,
                                 PhotoId = b.PhotoId,
                                 PictureName = b.PictureName,
                                 PicturePath = b.PicturePath,
                                 Tags = b.Tags
                             };
                    return photos;
            }
            catch(NullReferenceException)
            { 
                return Enumerable.Empty<PhotoDTO>().AsQueryable();
            }
 
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>PhotoDTO.</returns>
        public PhotoDTO GetById(int id)
        {
            try
            {
                var photo = Database.Photos.GetAll().Select(b =>
                new PhotoDTO()
                {
                    ProfileId = b.ProfileId,
                    UserName = b.UserName,
                    Likes = b.Likes,
                    PhotoId = b.PhotoId,
                    PictureName = b.PictureName,
                    PicturePath = b.PicturePath,
                    Tags = b.Tags
                }).SingleOrDefault(b => b.PhotoId == id);
                return photo;
            }
            catch (NullReferenceException)
            {
                return new PhotoDTO();
            }

        }

        /// <summary>Adds the new.</summary>
        /// <param name="photo">The photo.</param>
        public void AddNew(PhotoDTO photo)
        {
            Database.Photos.Create(new Photo
            {
                ProfileId = photo.ProfileId,
                UserName = photo.UserName,
                Likes = photo.Likes,
                PhotoId = photo.PhotoId,
                PictureName = photo.PictureName,
                PicturePath = photo.PicturePath,
                Tags = photo.Tags
            });

        }
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>Deletes the specified photo.</summary>
        /// <param name="photo">The photo.</param>
        public void Delete(PhotoDTO photo)
        {
                Database.Photos.Delete(new Photo
                {
                    ProfileId = photo.ProfileId,
                    UserName = photo.UserName,
                    Likes = photo.Likes,
                    PhotoId = photo.PhotoId,
                    PictureName = photo.PictureName,
                    PicturePath = photo.PicturePath,
                    Tags = photo.Tags
                });           
        }

        /// <summary>Adds the like.</summary>
        /// <param name="photo">The photo.</param>
        public void AddLike( PhotoDTO photo)
        {
           Database.Likes.Create(new Like
            {
                ProfileId = photo.ProfileId,
                PhotoId = photo.PhotoId
            });
            var like = Database.Likes.GetAll().OrderByDescending(l => l.LikeId).FirstOrDefault(l => l.PhotoId == photo.PhotoId);
        }

        /// <summary>Removes the like.</summary>
        /// <param name="photo">The photo.</param>
        public void RemoveLike(PhotoDTO photo)
        {
            var like = Database.Likes.GetAll().First(l => l.PhotoId == photo.PhotoId && l.ProfileId==photo.ProfileId);
            Database.Likes.Delete(new Like
            {
                ProfileId = like.ProfileId,
                PhotoId = like.PhotoId,
                LikeId = like.LikeId
            });
            
        }

        /// <summary>Seaches the photo.</summary>
        /// <param name="masTag">The mas tag.</param>
        /// <param name="isOld">if set to <c>true</c> [is old].</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns>IQueryable&lt;PhotoDTO&gt;.</returns>
        public IQueryable<PhotoDTO> SeachPhoto(int[] masTag, bool isOld,string profileId)
        {
            try
            {
                if (masTag == null && profileId=="0")
                {
                    var photos = from b in Database.Photos.GetAll()
                                 select new PhotoDTO()
                                 {
                                     ProfileId = b.ProfileId,
                                     UserName = b.UserName,
                                     Likes = b.Likes,
                                     PhotoId = b.PhotoId,
                                     PictureName = b.PictureName,
                                     PicturePath = b.PicturePath,
                                     Tags = b.Tags
                                 };

                    if (isOld == false)
                    {
                        return photos.OrderByDescending(p => p.PhotoId);
                    }
                    else
                    {
                        return photos.OrderBy(p => p.PhotoId);
                    }
                }
               else if (masTag != null && profileId == "0")
                {
                    List<PhotoDTO> listPhoto = new List<PhotoDTO>();
                    for (int i = 0; i < masTag.Length; i++)
                    {

                        int tag = masTag[i];
                        var photo = Database.Photos.GetAll().Select(b =>
                            new PhotoDTO()
                            {
                                ProfileId = b.ProfileId,
                                UserName = b.UserName,
                                Likes = b.Likes,
                                PhotoId = b.PhotoId,
                                PictureName = b.PictureName,
                                PicturePath = b.PicturePath,
                                Tags = b.Tags
                            }).FirstOrDefault(p => p.Tags.Any(c => c.TagId == tag));
                        if (photo != null)
                        {
                            listPhoto.Add(photo);
                        }
                        
                    }
                    if (isOld == false)
                    {
                        return listPhoto.AsQueryable().GroupBy(p => p.PhotoId).Select(p=>p.FirstOrDefault()).OrderByDescending(p=>p.PhotoId); 
                    }
                    else
                    {
                        return listPhoto.AsQueryable().GroupBy(p => p.PhotoId).Select(p => p.FirstOrDefault()).OrderBy(p => p.PhotoId);
                    }
                }
               else if (masTag != null && profileId != "0")
                {
                    List<PhotoDTO> listPhoto = new List<PhotoDTO>();
                    for (int i = 0; i < masTag.Length; i++)
                    {

                        int tag = masTag[i];
                        var photo = Database.Photos.GetAll().Select(b =>
                            new PhotoDTO()
                            {
                                ProfileId = b.ProfileId,
                                UserName = b.UserName,
                                Likes = b.Likes,
                                PhotoId = b.PhotoId,
                                PictureName = b.PictureName,
                                PicturePath = b.PicturePath,
                                Tags = b.Tags
                            }).FirstOrDefault(p => p.Tags.Any(c => c.TagId == tag) && p.ProfileId == profileId);
                        if (photo != null)
                        {
                            listPhoto.Add(photo);
                        }
                    }

                    if (isOld == false)
                    {
                        return listPhoto.AsQueryable().GroupBy(p => p.PhotoId).Select(p => p.FirstOrDefault()).OrderByDescending(p => p.PhotoId);
                    }
                    else
                    {
                        return listPhoto.AsQueryable().GroupBy(p => p.PhotoId).Select(p => p.FirstOrDefault()).OrderBy(p => p.PhotoId);
                    }
                }
                else
                {
                    var photo = Database.Photos.GetAll().Select(b =>
                        new PhotoDTO()
                        {
                            ProfileId = b.ProfileId,
                            UserName = b.UserName,
                            Likes = b.Likes,
                            PhotoId = b.PhotoId,
                            PictureName = b.PictureName,
                            PicturePath = b.PicturePath,
                            Tags = b.Tags
                        }).Where(p => p.ProfileId == profileId);
                    if (isOld == false)
                    {
                        return photo.OrderByDescending(p => p.PhotoId);
                    }
                    else
                    {
                        return photo.OrderBy(p => p.PhotoId);
                    }
                }
            }
            catch (NullReferenceException)
            {
                return Enumerable.Empty<PhotoDTO>().AsQueryable();
            }

        }

        /// <summary>Uploads photo to database.</summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns>PhotoDTO.</returns>
        public PhotoDTO UploadToDb(string path, string name, string profileId)
        {
            var user = Database.Profiles.GetAll().Select(b=>
            new ProfileDTO()
            {
              ProfileId=b.ProfileId,
              FirstName=b.FirstName,
              IsBlocked=b.IsBlocked,
              LastName=b.LastName,
              Role=b.Role,
              UserName=b.UserName
            }).FirstOrDefault(b => b.ProfileId== profileId);
            Random rand = new Random();
            Database.Photos.Create(new Photo()
            {
                ProfileId = profileId,
                UserName = user.UserName,
                PictureName = rand.Next()+name,
                PicturePath = "Data\\" + name
            });
            var photo= Database.Photos.GetAll().Select(b =>
                        new PhotoDTO()
                        {
                            ProfileId = b.ProfileId,
                            UserName = b.UserName,
                            Likes = b.Likes,
                            PhotoId = b.PhotoId,
                            PictureName = b.PictureName,
                            PicturePath = b.PicturePath,
                            Tags = b.Tags
                        }).OrderByDescending(p => p.PhotoId).FirstOrDefault(p => p.ProfileId == profileId);
            return photo;
        }

        /// <summary>Adds the tag to database into collection of tags in Photo.</summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="masTag">The mas tag.</param>
        public void AddTagToDb(int photoId, int[] masTag)
        {
            var photo = Database.Photos.GetAll().Select(b =>
                        new PhotoDTO()
                        {
                            ProfileId = b.ProfileId,
                            UserName = b.UserName,
                            Likes = b.Likes,
                            PhotoId = b.PhotoId,
                            PictureName = b.PictureName,
                            PicturePath = b.PicturePath,
                           // Tags = b.Tags
                        }).FirstOrDefault(p => p.PhotoId == photoId);

            ICollection<Tag> listTag = new Collection<Tag>();
            for (int i = 0; i < masTag.Length; i++)
            {
                int tagId = masTag[i];
                var tag = Database.Tags.GetAll().Select(t => new TagDTO() { Name = t.Name, TagId = t.TagId, Photos=t.Photos }).FirstOrDefault(t => t.TagId == tagId);
                if (tag != null)
                {

                     listTag.Add(new Tag()
                     {
                         Name = tag.Name,
                         TagId = tag.TagId
                     });
                   
                }
            }
            Database.Photos.Update(new Photo()
            {
                PhotoId = photo.PhotoId,
                ProfileId = photo.ProfileId,
                PictureName = photo.PictureName,
                PicturePath = photo.PicturePath,
                UserName = photo.UserName,
                Tags = listTag

            });


        }
    }
}
