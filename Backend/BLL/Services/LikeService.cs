using BLL.DTO;
using BLL.Interface;
using DAL.DataModel;
using DAL.Interfaces;
using System.Linq;
using System;

namespace BLL.Services
{

    /// <summary>Class LikeService.
    /// Implements the <see cref="BLL.Interface.IService{BLL.DTO.LikeDTO}"/></summary>
    /// <seealso cref="BLL.Interface.IService{BLL.DTO.LikeDTO}" />
    public class LikeService : IService<LikeDTO>
    {
        IUnitOfWork Database { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:BLL.Services.LikeService"/> class.</summary>
        /// <param name="uow">The uow.</param>
        public LikeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>Gets all.</summary>
        /// <returns>IQueryable&lt;LikeDTO&gt;.</returns>
        public IQueryable<LikeDTO> GetAll()
        {
            try
            {
                var likes = from b in Database.Likes.GetAll()
                            select new LikeDTO()
                            {
                                LikeId = b.LikeId,
                                ProfileId = b.ProfileId,
                                PhotoId = b.PhotoId
                            };
                return likes;
            }
            catch (NullReferenceException)
            {

                return Enumerable.Empty<LikeDTO>().AsQueryable();
            }

        }

        /// <summary>Adds the new.</summary>
        /// <param name="like">The like.</param>
        public void AddNew(LikeDTO like)
        {
            Database.Likes.Create(new Like { LikeId = like.LikeId, ProfileId = like.ProfileId, PhotoId = like.PhotoId });

        }
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>Deletes the specified like.</summary>
        /// <param name="like">The like.</param>
        public void Delete(LikeDTO like)
        {
            Database.Likes.Delete(new Like { LikeId = like.LikeId, ProfileId = like.ProfileId, PhotoId = like.PhotoId });
        }


    }
}
