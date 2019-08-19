using System;
using System.Linq;

namespace BLL.Interface
{
    public interface IService<T>:IDisposable
    {
        IQueryable<T> GetAll();
        void AddNew(T item);
        void Delete(T item);
       

    }
}
