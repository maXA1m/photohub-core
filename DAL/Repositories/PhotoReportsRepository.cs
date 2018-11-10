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
    /// Contains methods for processing DB entities in PhotoReports table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class PhotoReportsRepository : IRepository<PhotoReport>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoReportsRepository"/>.
        /// </summary>
        public PhotoReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="PhotoReportsRepository"/>.
        /// </summary>
        public IEnumerable<PhotoReport> GetAll()
        {
            return _context.PhotoReports;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="PhotoReportsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<PhotoReport> GetAll(int page, int pageSize)
        {
            return _context.PhotoReports.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="PhotoReport"/> by id (primary key).
        /// </summary>
        public PhotoReport Get(int id)
        {
            return _context.PhotoReports.Where(b => b.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="PhotoReport"/> by id (primary key).
        /// </summary>
        public async Task<PhotoReport> GetAsync(int id)
        {
            return await _context.PhotoReports.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="PhotoReport"/>(s) by predicate.
        /// </summary>
        public IEnumerable<PhotoReport> Find(Func<PhotoReport, bool> predicate)
        {
            return _context.PhotoReports.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="PhotoReport"/>.
        /// </summary>
        public void Create(PhotoReport item)
        {
            _context.PhotoReports.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="PhotoReport"/>.
        /// </summary>
        public async Task CreateAsync(PhotoReport item)
        {
            await _context.PhotoReports.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="PhotoReport"/>.
        /// </summary>
        public void Update(PhotoReport item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="PhotoReport"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.PhotoReports.Find(id);

            if (item != null)
            {
                _context.PhotoReports.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="PhotoReport"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.PhotoReports.FindAsync(id);

            if (item != null)
            {
                _context.PhotoReports.Remove(item);
            }
        }

        #endregion
    }
}