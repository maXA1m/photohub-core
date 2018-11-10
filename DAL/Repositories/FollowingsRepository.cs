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
    /// Contains methods for processing DB entities in Followings table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class FollowingsRepository : IRepository<Following>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowingsRepository"/>.
        /// </summary>
        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="FollowingsRepository"/>.
        /// </summary>
        public IEnumerable<Following> GetAll()
        {
            return _context.Followings;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="FollowingsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Following> GetAll(int page, int pageSize)
        {
            return _context.Followings.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Following"/> by id (primary key).
        /// </summary>
        public Following Get(int id)
        {
            return _context.Followings.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Following"/> by id (primary key).
        /// </summary>
        public async Task<Following> GetAsync(int id)
        {
            return await _context.Followings.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="Following"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Following> Find(Func<Following, bool> predicate)
        {
            return _context.Followings.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="Following"/>.
        /// </summary>
        public void Create(Following item)
        {
            _context.Followings.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Following"/>.
        /// </summary>
        public async Task CreateAsync(Following item)
        {
            await _context.Followings.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Following"/>.
        /// </summary>
        public void Update(Following item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Following"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Followings.Find(id);

            if (item != null)
            {
                _context.Followings.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Following"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Followings.FindAsync(id);

            if (item != null)
            {
                _context.Followings.Remove(item);
            }
        }

        #endregion
    }
}