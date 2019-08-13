using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Delete(T item);
        void Update(T item);
    }
}
