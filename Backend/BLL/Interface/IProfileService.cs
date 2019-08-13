using BLL.DTO;

namespace BLL.Interface
{
    public interface IProfileService : IService<ProfileDTO>
    {
        ProfileDTO GetProfileByUserId(ProfileDTO profile);
        void Update(ProfileDTO item);
        void BlockProfile(ProfileDTO item);
        void UnblockProfile(ProfileDTO item);
    }
}
