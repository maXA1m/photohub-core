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
    public class ApertureRepository : IRepository<Aperture>
    {
        private readonly ApplicationDbContext _context;

        public ApertureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Aperture> GetAll(int page, int pageSize)
        {
            return _context.Apertures.OrderBy(a => a.Id).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Aperture> Find(Func<Aperture, bool> predicate)
        {
            return _context.Apertures.OrderBy(a => a.Id).Where(predicate);
        }

        public Aperture Get(int id)
        {
            return _context.Apertures.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Aperture> GetAsync(int id)
        {
            return await _context.Apertures.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Aperture item)
        {
            _context.Apertures.Add(item);
        }
        public async Task CreateAsync(Aperture item)
        {
            await _context.Apertures.AddAsync(item);
        }

        public void Update(Aperture item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Aperture aperture = _context.Apertures.Find(id);
            if (aperture != null)
                _context.Apertures.Remove(aperture);
        }
        public async Task DeleteAsync(int id)
        {
            Aperture aperture = await _context.Apertures.FindAsync(id);
            if (aperture != null)
                _context.Apertures.Remove(aperture);
        }
    }
}
