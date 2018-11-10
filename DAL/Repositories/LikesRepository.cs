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
    /// Contains methods for processing DB entities in Likes table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class LikesRepository : IRepository<Like>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="LikesRepository"/>.
        /// </summary>
        public LikesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="LikesRepository"/>.
        /// </summary>
        public IEnumerable<Like> GetAll()
        {
            return _context.Likes.OrderBy(l => l.Date);
        }

        /// <summary>
        /// Method for fetching all data from <see cref="LikesRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Like> GetAll(int page, int pageSize)
        {
            return _context.Likes.OrderBy(l => l.Date).Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Like"/> by id (primary key).
        /// </summary>
        public Like Get(int id)
        {
            return _context.Likes.Where(l => l.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Like"/> by id (primary key).
        /// </summary>
        public async Task<Like> GetAsync(int id)
        {
            return await _context.Likes.Where(l => l.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="Like"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Like> Find(Func<Like, bool> predicate)
        {
            return _context.Likes.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="Like"/>.
        /// </summary>
        public void Create(Like item)
        {
            _context.Likes.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Like"/>.
        /// </summary>
        public async Task CreateAsync(Like item)
        {
            await _context.Likes.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Like"/>.
        /// </summary>
        public void Update(Like item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Like"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Likes.Find(id);

            if (item != null)
            {
                _context.Likes.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Like"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Likes.FindAsync(id);

            if (item != null)
            {
                _context.Likes.Remove(item);
            }
        }

        #endregion
    }
}