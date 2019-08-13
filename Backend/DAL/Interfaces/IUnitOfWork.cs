using DAL.DataModel;
using DAL.Identity;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPhotoRepository Photos { get; }
        IRepository<Profile> Profiles { get; }
        IRepository<Tag> Tags { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Like> Likes { get; }

        void Save();
    }
}
