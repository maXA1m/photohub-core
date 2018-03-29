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
    public class PhotosRepository : IRepository<Photo>
    {
        private readonly ApplicationDbContext _context;

        public PhotosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Photo> GetAll(int page, int pageSize)
        {
            return _context.Photos
                    .Include(p => p.Owner)
                    .Include(p => p.Filter)
                    .Include(p => p.Aperture)
                    .Include(p => p.Exposure)
                    .Include(p => p.Iso)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.Owner)
                    .Include(p => p.Likes)
                        .ThenInclude(l => l.Owner)
                    .OrderByDescending(p => p.Date)
                    .Skip(page * pageSize).Take(pageSize);
        }

        public Photo Get(int id)
        {
            return _context.Photos
                    .Include(p => p.Owner)
                    .Include(p => p.Filter)
                    .Include(p => p.Aperture)
                    .Include(p => p.Exposure)
                    .Include(p => p.Iso)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.Owner)
                    .Include(p => p.Likes)
                        .ThenInclude(l => l.Owner)
                    .Where(p => p.Id == id).FirstOrDefault();
        }
        public async Task<Photo> GetAsync(int id)
        {
            return await _context.Photos
                            .Include(p => p.Owner)
                            .Include(p => p.Filter)
                            .Include(p => p.Aperture)
                            .Include(p => p.Exposure)
                            .Include(p => p.Iso)
                            .Include(p => p.Comments)
                                .ThenInclude(c => c.Owner)
                            .Include(p => p.Likes)
                                .ThenInclude(l => l.Owner)
                            .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Photo> Find(Func<Photo, bool> predicate)
        {
            return _context.Photos
                    .Include(p => p.Owner)
                    .Include(p => p.Filter)
                    .Include(p => p.Aperture)
                    .Include(p => p.Exposure)
                    .Include(p => p.Iso)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.Owner)
                    .Include(p => p.Likes)
                        .ThenInclude(l => l.Owner)
                    .Where(predicate);
        }

        public void Create(Photo item)
        {
            _context.Photos.Add(item);
        }
        public async Task CreateAsync(Photo item)
        {
            await _context.Photos.AddAsync(item);
        }

        public void Update(Photo item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Photo photo = _context.Photos.Find(id);
            if (photo != null)
                _context.Photos.Remove(photo);
        }
        public async Task DeleteAsync(int id)
        {
            Photo photo = await _context.Photos.FindAsync(id);
            if (photo != null)
                _context.Photos.Remove(photo);
        }
    }
}