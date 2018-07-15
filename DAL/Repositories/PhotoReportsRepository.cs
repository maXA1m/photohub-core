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
    public class PhotoReportsRepository : IRepository<PhotoReport>
    {
        private readonly ApplicationDbContext _context;

        public PhotoReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PhotoReport> GetAll()
        {
            return _context.PhotoReports
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter);
        }

        public IEnumerable<PhotoReport> GetAll(int page, int pageSize)
        {
            return _context.PhotoReports
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Skip(page * pageSize).Take(pageSize);
        }

        public PhotoReport Get(int id)
        {
            return _context.PhotoReports
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Where(b => b.Id == id).FirstOrDefault();
        }
        public async Task<PhotoReport> GetAsync(int id)
        {
            return await _context.PhotoReports
                                    .Include(b => b.User)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<PhotoReport> Find(Func<PhotoReport, bool> predicate)
        {
            return _context.PhotoReports
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Where(predicate);
        }

        public void Create(PhotoReport item)
        {
            _context.PhotoReports.Add(item);
        }
        public async Task CreateAsync(PhotoReport item)
        {
            await _context.PhotoReports.AddAsync(item);
        }

        public void Update(PhotoReport item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PhotoReport item = _context.PhotoReports.Find(id);
            if (item != null)
                _context.PhotoReports.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            PhotoReport item = await _context.PhotoReports.FindAsync(id);
            if (item != null)
                _context.PhotoReports.Remove(item);
        }
    }
}