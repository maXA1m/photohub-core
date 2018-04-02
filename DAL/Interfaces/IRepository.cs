#region using System
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace PhotoHub.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int page, int pageSize);

        T Get(int id);
        Task<T> GetAsync(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);

        void Create(T item);
        Task CreateAsync(T item);

        void Update(T item);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
