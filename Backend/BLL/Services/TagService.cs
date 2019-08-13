using BLL.DTO;
using BLL.Interface;
using DAL.DataModel;
using DAL.Interfaces;
using System.Linq;
using System;

namespace BLL.Services
{
    /// <summary>Class TagService.
    /// Implements the <see cref="BLL.Interface.IService{BLL.DTO.TagDTO}"/></summary>
    /// <seealso cref="BLL.Interface.IService{BLL.DTO.TagDTO}" />
    public class TagService : IService<TagDTO>
    {
        IUnitOfWork Database { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:BLL.Services.TagService"/> class.</summary>
        /// <param name="uow">The uow.</param>
        public TagService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>Gets all tags.</summary>
        /// <returns>IQueryable&lt;TagDTO&gt;.</returns>
        public IQueryable<TagDTO> GetAll()
        {
            try
            {
                var tags = from b in Database.Tags.GetAll()
                           select new TagDTO()
                           {
                               Name = b.Name,
                               TagId = b.TagId,
                               Photos = b.Photos
                           };
                return tags;
            }
            catch (System.Exception)
            {

                return Enumerable.Empty<TagDTO>().AsQueryable();
            }

        }

        /// <summary>Adds the new tag.</summary>
        /// <param name="tag">The tag.</param>
        public void AddNew(TagDTO tag)
        {
            Database.Tags.Create(new Tag
            {
                Name = tag.Name,
                TagId = tag.TagId,
                Photos = tag.Photos
            });

        }
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>Deletes the specified tag.</summary>
        /// <param name="tag">The tag.</param>
        public void Delete(TagDTO tag)
        {
            Database.Tags.Delete(new Tag
            {
                Name = tag.Name,
                TagId = tag.TagId,
                Photos = tag.Photos
            });
        }


    }
}
