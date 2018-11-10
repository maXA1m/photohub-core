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
    /// Contains methods for processing DB entities in Bookmarks table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class BookmarksRepository : IRepository<Bookmark>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarksRepository"/>.
        /// </summary>
        public BookmarksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="BookmarksRepository"/>.
        /// </summary>
        public IEnumerable<Bookmark> GetAll()
        {
            return _context.Bookmarks;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="BookmarksRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Bookmark> GetAll(int page, int pageSize)
        {
            return _context.Bookmarks.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Bookmark"/> by id (primary key).
        /// </summary>
        public Bookmark Get(int id)
        {
            return _context.Bookmarks.Where(b => b.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Bookmark"/> by id (primary key).
        /// </summary>
        public async Task<Bookmark> GetAsync(int id)
        {
            return await _context.Bookmarks.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="Bookmark"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Bookmark> Find(Func<Bookmark, bool> predicate)
        {
            return _context.Bookmarks.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="Bookmark"/>.
        /// </summary>
        public void Create(Bookmark item)
        {
            _context.Bookmarks.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Bookmark"/>.
        /// </summary>
        public async Task CreateAsync(Bookmark item)
        {
            await _context.Bookmarks.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Bookmark"/>.
        /// </summary>
        public void Update(Bookmark item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Bookmark"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Bookmarks.Find(id);

            if (item != null)
            {
                _context.Bookmarks.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Bookmark"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Bookmarks.FindAsync(id);

            if (item != null)
            {
                _context.Bookmarks.Remove(item);
            }
        }

        #endregion
    }
}