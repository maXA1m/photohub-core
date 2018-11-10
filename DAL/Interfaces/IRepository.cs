using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoHub.DAL.Interfaces
{
    /// <summary>
    /// Interface for DB repositories.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method for fetching all data from table.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Method for fetching all data from table with paggination.
        /// </summary>
        IEnumerable<T> GetAll(int page, int pageSize);

        /// <summary>
        /// Method for fetching entity by id (primary key).
        /// </summary>
        T Get(int id);

        /// <summary>
        /// Async method for fetching entity by id (primary key).
        /// </summary>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Method for fetching entity(ies) by predicate.
        /// </summary>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Method for creating entity.
        /// </summary>
        void Create(T item);

        /// <summary>
        /// Async method for creating entity.
        /// </summary>
        Task CreateAsync(T item);

        /// <summary>
        /// Method for updating entity.
        /// </summary>
        void Update(T item);

        /// <summary>
        /// Method for deleting entity.
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Async method for deleting entity.
        /// </summary>
        Task DeleteAsync(int id);
    }
}
