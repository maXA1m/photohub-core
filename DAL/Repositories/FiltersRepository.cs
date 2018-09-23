using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Repositories
{
    /// <summary>
    /// Contains methods for processing DB entities in Confirmations table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class FiltersRepository : IRepository<Filter>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FiltersRepository"/>.
        /// </summary>
        public FiltersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="FiltersRepository"/>.
        /// </summary>
        public IEnumerable<Filter> GetAll()
        {
            return _context.Filters;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="FiltersRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Filter> GetAll(int page, int pageSize)
        {
            return _context.Filters.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Filter"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Filter> Find(Func<Filter, bool> predicate)
        {
            return _context.Filters.Where(predicate);
        }

        /// <summary>
        /// Method for fetching <see cref="Filter"/> by id (primary key).
        /// </summary>
        public Filter Get(int id)
        {
            return _context.Filters.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Filter"/> by id (primary key).
        /// </summary>
        public async Task<Filter> GetAsync(int id)
        {
            return await _context.Filters.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for creating <see cref="Filter"/>.
        /// </summary>
        public void Create(Filter item)
        {
            _context.Filters.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Filter"/>.
        /// </summary>
        public async Task CreateAsync(Filter item)
        {
            await _context.Filters.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Filter"/>.
        /// </summary>
        public void Update(Filter item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Filter"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Filters.Find(id);

            if (item != null)
            {
                _context.Filters.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Filter"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Filters.FindAsync(id);

            if (item != null)
            {
                _context.Filters.Remove(item);
            }
        }

        #endregion
    }
}
