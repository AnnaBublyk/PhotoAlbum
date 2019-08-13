using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interface
{
   public interface IService<T>:IDisposable
    {
        IQueryable<T> GetAll();
        void AddNew(T item);
        void Delete(T item);
       

    }
}
