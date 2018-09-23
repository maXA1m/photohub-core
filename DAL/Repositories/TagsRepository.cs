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
    /// Contains methods for processing DB entities in Tags table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class TagsRepository : IRepository<Tag>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsRepository"/>.
        /// </summary>
        public TagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="TagsRepository"/>.
        /// </summary>
        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.OrderBy(i => i.Id);
        }

        /// <summary>
        /// Method for fetching all data from <see cref="TagsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<Tag> GetAll(int page, int pageSize)
        {
            return _context.Tags.OrderBy(i => i.Id).Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="Tag"/>(s) by predicate.
        /// </summary>
        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return _context.Tags.OrderBy(i => i.Id).Where(predicate);
        }

        /// <summary>
        /// Method for fetching <see cref="Tag"/> by id (primary key).
        /// </summary>
        public Tag Get(int id)
        {
            return _context.Tags.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="Tag"/> by id (primary key).
        /// </summary>
        public async Task<Tag> GetAsync(int id)
        {
            return await _context.Tags.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for creating <see cref="Tag"/>.
        /// </summary>
        public void Create(Tag item)
        {
            _context.Tags.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="Tag"/>.
        /// </summary>
        public async Task CreateAsync(Tag item)
        {
            await _context.Tags.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="Tag"/>.
        /// </summary>
        public void Update(Tag item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="Tag"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.Tags.Find(id);

            if (item != null)
            {
                _context.Tags.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="Tag"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Tags.FindAsync(id);

            if (item != null)
            {
                _context.Tags.Remove(item);
            }
        }

        #endregion
    }
}
