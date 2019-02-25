using System;

namespace eFolio.BL
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}