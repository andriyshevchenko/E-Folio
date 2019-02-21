using System;
using System.Collections.Generic;

namespace e_folio.data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItemsList();
        IEnumerable<T> Search(string request);
        T GetItem(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}