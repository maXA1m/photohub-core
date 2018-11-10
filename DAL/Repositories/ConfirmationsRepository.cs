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
    public class ConfirmationsRepository : IRepository<Confirmed>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmationsRepository"/>.
        /// </summary>
        public ConfirmationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="ConfirmationsRepository"/>.
        /// </summary>
        public IEnumerable<Confirmed> GetAll()
        {
            return _context.Confirmed;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="ConfirmationsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Confirmed> GetAll(int page, int pageSize)
        {
            return _context.Confirmed.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Confirmed"/> by id (primary key).
        /// </summary>
        public Confirmed Get(int id)
        {
            return _context.Confirmed.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Confirmed"/> by id (primary key).
        /// </summary>
        public async Task<Confirmed> GetAsync(int id)
        {
            return await _context.Confirmed.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="Confirmed"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Confirmed> Find(Func<Confirmed, bool> predicate)
        {
            return _context.Confirmed.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="Confirmed"/>.
        /// </summary>
        public void Create(Confirmed item)
        {
            _context.Confirmed.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Confirmed"/>.
        /// </summary>
        public async Task CreateAsync(Confirmed item)
        {
            await _context.Confirmed.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Confirmed"/>.
        /// </summary>
        public void Update(Confirmed item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Confirmed"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Confirmed.Find(id);

            if (item != null)
            {
                _context.Confirmed.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Confirmed"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Confirmed.FindAsync(id);

            if (item != null)
            {
                _context.Confirmed.Remove(item);
            }
        }

        #endregion
    }
}