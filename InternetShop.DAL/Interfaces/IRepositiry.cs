using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShop.DAL.Interfaces
{
    public interface IRepositiry<T> where T:class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(string id);
    }
}
