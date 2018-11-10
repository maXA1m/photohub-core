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
    /// Contains methods for processing DB entities in Blockings table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class BlockingsRepository : IRepository<BlackList>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockingsRepository"/>.
        /// </summary>
        public BlockingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="BlockingsRepository"/>.
        /// </summary>
        public IEnumerable<BlackList> GetAll()
        {
            return _context.BlackLists;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="BlockingsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<BlackList> GetAll(int page, int pageSize)
        {
            return _context.BlackLists.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="BlackList"/> by id (primary key).
        /// </summary>
        public BlackList Get(int id)
        {
            return _context.BlackLists.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="BlackList"/> by id (primary key).
        /// </summary>
        public async Task<BlackList> GetAsync(int id)
        {
            return await _context.BlackLists.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="BlackList"/>(s) by predicate.
        /// </summary>
        public IEnumerable<BlackList> Find(Func<BlackList, bool> predicate)
        {
            return _context.BlackLists.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="BlackList"/>.
        /// </summary>
        public void Create(BlackList item)
        {
            _context.BlackLists.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="BlackList"/>.
        /// </summary>
        public async Task CreateAsync(BlackList item)
        {
            await _context.BlackLists.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="BlackList"/>.
        /// </summary>
        public void Update(BlackList item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="BlackList"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.BlackLists.Find(id);

            if (item != null)
            {
                _context.BlackLists.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="BlackList"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.BlackLists.FindAsync(id);

            if (item != null)
            {
                _context.BlackLists.Remove(item);
            }
        }

        #endregion
    }
}