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
    public class BookmarksRepository : IRepository<Bookmark>
    {
        private readonly ApplicationDbContext _context;

        public BookmarksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bookmark> GetAll(int page, int pageSize)
        {
            return _context.Bookmarks
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Aperture)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Exposure)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Iso)
                    .Skip(page * pageSize).Take(pageSize);
        }

        public Bookmark Get(int id)
        {
            return _context.Bookmarks
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Aperture)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Exposure)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Iso)
                    .Where(b => b.Id == id).FirstOrDefault();
        }
        public async Task<Bookmark> GetAsync(int id)
        {
            return await _context.Bookmarks
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

        public IEnumerable<Bookmark> Find(Func<Bookmark, bool> predicate)
        {
            return _context.Bookmarks
                    .Include(b => b.User)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Comments)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Likes)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Owner)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Filter)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Aperture)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Exposure)
                    .Include(b => b.Photo)
                        .ThenInclude(p => p.Iso)
                    .Where(predicate);
        }

        public void Create(Bookmark item)
        {
            _context.Bookmarks.Add(item);
        }
        public async Task CreateAsync(Bookmark item)
        {
            await _context.Bookmarks.AddAsync(item);
        }

        public void Update(Bookmark item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Bookmark bookmark = _context.Bookmarks.Find(id);
            if (bookmark != null)
                _context.Bookmarks.Remove(bookmark);
        }
        public async Task DeleteAsync(int id)
        {
            Bookmark bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark != null)
                _context.Bookmarks.Remove(bookmark);
        }
    }
}