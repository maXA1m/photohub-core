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
    public class PhotoViewsRepository : IRepository<PhotoView>
    {
        private readonly ApplicationDbContext _context;

        public PhotoViewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PhotoView> GetAll(int page, int pageSize)
        {
            return _context.PhotoViews.Skip(page * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<PhotoView>> GetAllAsync(int page, int pageSize)
        {
            return await _context.PhotoViews.Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<PhotoView> Find(Func<PhotoView, bool> predicate)
        {
            return _context.PhotoViews.Where(predicate);
        }

        public PhotoView Get(int id)
        {
            return _context.PhotoViews.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<PhotoView> GetAsync(int id)
        {
            return await _context.PhotoViews.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(PhotoView item)
        {
            _context.PhotoViews.Add(item);
        }
        public async Task CreateAsync(PhotoView item)
        {
            await _context.PhotoViews.AddAsync(item);
        }

        public void Update(PhotoView item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PhotoView photoView = _context.PhotoViews.Find(id);
            if (photoView != null)
                _context.PhotoViews.Remove(photoView);
        }
        public async Task DeleteAsync(int id)
        {
            PhotoView photoView = await _context.PhotoViews.FindAsync(id);
            if (photoView != null)
                _context.PhotoViews.Remove(photoView);
        }
    }
}
