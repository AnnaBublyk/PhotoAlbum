using BLL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Interface
{
    public interface IPhotoService:IService<PhotoDTO>
    {
        void AddLike(PhotoDTO item);
        void RemoveLike(PhotoDTO item);
        IQueryable<PhotoDTO> SeachPhoto(int[] masTag, bool isOld, string profileId);
        PhotoDTO UploadToDb(string path, string name, string profileId);
        void AddTagToDb(int photoId, int[] masTag);
        PhotoDTO GetById(int id);
    }
}
