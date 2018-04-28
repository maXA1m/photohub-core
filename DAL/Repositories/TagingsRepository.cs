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
    public class TagingsRepository : IRepository<Taging>
    {
        private readonly ApplicationDbContext _context;

        public TagingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Taging> GetAll()
        {
            return _context.Tagings
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Include(t => t.Tag)
                            .OrderBy(i => i.Id);
        }

        public IEnumerable<Taging> GetAll(int page, int pageSize)
        {
            return _context.Tagings
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Include(t => t.Tag)
                            .OrderBy(i => i.Id)
                            .Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Taging> Find(Func<Taging, bool> predicate)
        {
            return _context.Tagings
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Include(t => t.Tag)
                            .OrderBy(i => i.Id)
                            .Where(predicate);
        }

        public Taging Get(int id)
        {
            return _context.Tagings
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Include(t => t.Tag)
                            .Where(c => c.Id == id)
                            .FirstOrDefault();
        }
        public async Task<Taging> GetAsync(int id)
        {
            return await _context.Tagings
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Comments)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Likes)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Owner)
                            .Include(b => b.Photo)
                                .ThenInclude(p => p.Filter)
                            .Include(t => t.Tag)
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public void Create(Taging item)
        {
            _context.Tagings.Add(item);
        }
        public async Task CreateAsync(Taging item)
        {
            await _context.Tagings.AddAsync(item);
        }

        public void Update(Taging item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Taging item = _context.Tagings.Find(id);
            if (item != null)
                _context.Tagings.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            Taging item = await _context.Tagings.FindAsync(id);
            if (item != null)
                _context.Tagings.Remove(item);
        }
    }
}
