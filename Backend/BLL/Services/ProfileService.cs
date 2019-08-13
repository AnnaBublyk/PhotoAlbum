using BLL.DTO;
using BLL.Interface;
using DAL.DataModel;
using DAL.Interfaces;
using System;
using System.Linq;

namespace BLL.Services
{
    /// <summary>Class ProfileService.
    /// Implements the <see cref="BLL.Interface.IProfileService"/></summary>
    /// <seealso cref="BLL.Interface.IProfileService" />
    public class ProfileService : IProfileService
    {
        IUnitOfWork Database { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:BLL.Services.ProfileService"/> class.</summary>
        /// <param name="uow">The uow.</param>
        public ProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>Gets all profiles.</summary>
        /// <returns>IQueryable&lt;ProfileDTO&gt;.</returns>
        public IQueryable<ProfileDTO> GetAll()
        {
            try
            {
                var profiles = from b in Database.Profiles.GetAll()
                               select new ProfileDTO()
                               {
                                   ProfileId = b.ProfileId,
                                   FirstName = b.FirstName,
                                   LastName = b.LastName,
                                   UserName = b.UserName,
                                   IsBlocked = b.IsBlocked,
                                   PhotosDTO = b.Photos
                               };
                return profiles;
            }
            catch (NullReferenceException)
            {
                return Enumerable.Empty<ProfileDTO>().AsQueryable();
            }

        }

        /// <summary>Adds the new profile.</summary>
        /// <param name="profile">The profile.</param>
        public void AddNew(ProfileDTO profile)
        {
            Database.Profiles.Create(new Profile
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                IsBlocked = profile.IsBlocked,
                Photos = profile.PhotosDTO
            });

        }
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>Deletes the specified profile.</summary>
        /// <param name="profile">The profile.</param>
        public void Delete(ProfileDTO profile)
        {
            Database.Profiles.Delete(new Profile
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                IsBlocked = profile.IsBlocked,
                Photos = profile.PhotosDTO
            });
        }

        /// <summary>Gets the profile by user identifier.</summary>
        /// <param name="prof">The profile.</param>
        /// <returns>ProfileDTO.</returns>
        public ProfileDTO GetProfileByUserId(ProfileDTO prof)
        {
            try
            {
                var profile = Database.Profiles.GetAll().Select(b =>
                                   new ProfileDTO()
                                   {
                                       ProfileId = b.ProfileId,
                                       FirstName = b.FirstName,
                                       LastName = b.LastName,
                                       UserName = b.UserName,
                                       IsBlocked = b.IsBlocked,
                                       PhotosDTO = b.Photos,
                                       Role = b.Role
                                   }).SingleOrDefault(b => b.ProfileId == prof.ProfileId);
                return profile;
               
            }
            catch (NullReferenceException)
            {

                return new ProfileDTO();
            }

        }

        /// <summary>Updates the specified profile.</summary>
        /// <param name="profile">The profile.</param>
        public void Update(ProfileDTO profile)
        {
            Database.Profiles.Update(new Profile
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                IsBlocked = profile.IsBlocked,
                Photos = profile.PhotosDTO
            });
        }

        /// <summary>Blocks the profile.</summary>
        /// <param name="profile">The profile.</param>
        public void BlockProfile(ProfileDTO profile)
        {
            Database.Profiles.Update(new Profile
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                IsBlocked = true,
                Photos = profile.PhotosDTO
            });
        }
        /// <summary>Unblocks the profile.</summary>
        /// <param name="profile">The profile.</param>
        public void UnblockProfile(ProfileDTO profile)
        {
            Database.Profiles.Update(new Profile
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                IsBlocked = false,
                Photos = profile.PhotosDTO
            });
        }
    }
}
