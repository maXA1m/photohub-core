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
    /// Contains methods for processing DB entities in Tagings table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class TagingsRepository : IRepository<Taging>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagingsRepository"/>.
        /// </summary>
        public TagingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="TagingsRepository"/>.
        /// </summary>
        public IEnumerable<Taging> GetAll()
        {
            return _context.Tagings.OrderBy(i => i.Id);
        }

        /// <summary>
        /// Method for fetching all data from <see cref="TagingsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Taging> GetAll(int page, int pageSize)
        {
            return _context.Tagings.OrderBy(i => i.Id).Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Taging"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Taging> Find(Func<Taging, bool> predicate)
        {
            return _context.Tagings.OrderBy(i => i.Id).Where(predicate);
        }

        /// <summary>
        /// Method for fetching <see cref="Taging"/> by id (primary key).
        /// </summary>
        public Taging Get(int id)
        {
            return _context.Tagings.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Taging"/> by id (primary key).
        /// </summary>
        public async Task<Taging> GetAsync(int id)
        {
            return await _context.Tagings.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for creating <see cref="Taging"/>.
        /// </summary>
        public void Create(Taging item)
        {
            _context.Tagings.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Taging"/>.
        /// </summary>
        public async Task CreateAsync(Taging item)
        {
            await _context.Tagings.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Taging"/>.
        /// </summary>
        public void Update(Taging item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Taging"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Tagings.Find(id);

            if (item != null)
            {
                _context.Tagings.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Taging"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Tagings.FindAsync(id);

            if (item != null)
            {
                _context.Tagings.Remove(item);
            }
        }

        #endregion
    }
}
