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
    /// Contains methods for processing DB entities in UserReports table.
    /// Implementation of <see cref="IRepository"/>.
    /// </summary>
    public class UserReportsRepository : IRepository<UserReport>
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserReportsRepository"/>.
        /// </summary>
        public UserReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Method for fetching all data from <see cref="UserReportsRepository"/>.
        /// </summary>
        public IEnumerable<UserReport> GetAll()
        {
            return _context.UserReports;
        }

        /// <summary>
        /// Method for fetching all data from <see cref="UserReportsRepository"/> with paggination.
        /// </summary>
        public IEnumerable<UserReport> GetAll(int page, int pageSize)
        {
            return _context.UserReports.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Method for fetching <see cref="UserReport"/> by id (primary key).
        /// </summary>
        public UserReport Get(int id)
        {
            return _context.UserReports.Where(b => b.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Async method for fetching <see cref="UserReport"/> by id (primary key).
        /// </summary>
        public async Task<UserReport> GetAsync(int id)
        {
            return await _context.UserReports.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method for fetching <see cref="UserReport"/>(s) by predicate.
        /// </summary>
        public IEnumerable<UserReport> Find(Func<UserReport, bool> predicate)
        {
            return _context.UserReports.Where(predicate);
        }

        /// <summary>
        /// Method for creating <see cref="UserReport"/>.
        /// </summary>
        public void Create(UserReport item)
        {
            _context.UserReports.Add(item);
        }

        /// <summary>
        /// Async method for creating <see cref="UserReport"/>.
        /// </summary>
        public async Task CreateAsync(UserReport item)
        {
            await _context.UserReports.AddAsync(item);
        }

        /// <summary>
        /// Method for updating <see cref="UserReport"/>.
        /// </summary>
        public void Update(UserReport item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Method for deleting <see cref="UserReport"/>.
        /// </summary>
        public void Delete(int id)
        {
            var item = _context.UserReports.Find(id);

            if (item != null)
            {
                _context.UserReports.Remove(item);
            }
        }

        /// <summary>
        /// Async method for deleting <see cref="UserReport"/>.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var item = await _context.UserReports.FindAsync(id);

            if (item != null)
            {
                _context.UserReports.Remove(item);
            }
        }

        #endregion
    }
}