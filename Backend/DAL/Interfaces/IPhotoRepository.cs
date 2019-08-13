using DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        Photo GetById(int id);
        void Save();
    }
}
