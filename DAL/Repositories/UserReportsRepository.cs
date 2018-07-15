#region using System/Microsoft
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;
#endregion

namespace PhotoHub.DAL.Repositories
{
    public class UserReportsRepository : IRepository<UserReport>
    {
        private readonly ApplicationDbContext _context;

        public UserReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserReport> GetAll()
        {
            return _context.UserReports
                    .Include(b => b.User)
                    .Include(b => b.ReportedUser);
        }

        public IEnumerable<UserReport> GetAll(int page, int pageSize)
        {
            return _context.UserReports
                    .Include(b => b.User)
                    .Include(b => b.ReportedUser)
                    .Skip(page * pageSize).Take(pageSize);
        }

        public UserReport Get(int id)
        {
            return _context.UserReports
                    .Include(b => b.User)
                    .Include(b => b.ReportedUser)
                    .Where(b => b.Id == id).FirstOrDefault();
        }
        public async Task<UserReport> GetAsync(int id)
        {
            return await _context.UserReports
                            .Include(b => b.User)
                            .Include(b => b.ReportedUser)
                            .Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<UserReport> Find(Func<UserReport, bool> predicate)
        {
            return _context.UserReports
                    .Include(b => b.User)
                    .Include(b => b.ReportedUser)
                    .Where(predicate);
        }

        public void Create(UserReport item)
        {
            _context.UserReports.Add(item);
        }
        public async Task CreateAsync(UserReport item)
        {
            await _context.UserReports.AddAsync(item);
        }

        public void Update(UserReport item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserReport item = _context.UserReports.Find(id);
            if (item != null)
                _context.UserReports.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            UserReport item = await _context.UserReports.FindAsync(id);
            if (item != null)
                _context.UserReports.Remove(item);
        }
    }
}