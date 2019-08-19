using DAL.DataModel;

namespace DAL.Interfaces
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        Photo GetById(int id);
        void Save();
    }
}
