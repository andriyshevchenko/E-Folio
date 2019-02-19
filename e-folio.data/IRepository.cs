using e_folio.core.Entities;
using System;
using System.Collections.Generic;

namespace e_folio.data
{
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItemsList();
        IEnumerable<T> Search(string request);
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}