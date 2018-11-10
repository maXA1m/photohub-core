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
    /// Contains methods for processing DB entities in Comments table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class CommentsRepository : IRepository<Comment>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsRepository"/>.
        /// </summary>
        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="CommentsRepository"/>.
        /// </summary>
        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.OrderBy(c => c.Date);
        }

        /// <summary>
        /// Method for fetching all data from <see cref="CommentsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Comment> GetAll(int page, int pageSize)
        {
            return _context.Comments.OrderBy(c => c.Date).Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Comment"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return _context.Comments.Where(predicate);
        }

        /// <summary>
        /// Method for fetching <see cref="Comment"/> by id (primary key).
        /// </summary>
        public Comment Get(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Comment"/> by id (primary key).
        /// </summary>
        public async Task<Comment> GetAsync(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for creating <see cref="Comment"/>.
        /// </summary>
        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Comment"/>.
        /// </summary>
        public async Task CreateAsync(Comment item)
        {
            await _context.Comments.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Comment"/>.
        /// </summary>
        public void Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Comment"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Comments.Find(id);

            if (item != null)
            {
                _context.Comments.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Comment"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Comments.FindAsync(id);

            if (item != null)
            {
                _context.Comments.Remove(item);
            }
        }

        #endregion
    }
}