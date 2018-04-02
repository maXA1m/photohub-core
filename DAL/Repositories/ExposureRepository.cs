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
    public class ExposureRepository : IRepository<Exposure>
    {
        private readonly ApplicationDbContext _context;

        public ExposureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Exposure> GetAll()
        {
            return _context.Exposures.OrderBy(e => e.Id);
        }

        public IEnumerable<Exposure> GetAll(int page, int pageSize)
        {
            return _context.Exposures.OrderBy(e => e.Id).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Exposure> Find(Func<Exposure, bool> predicate)
        {
            return _context.Exposures.OrderBy(e => e.Id).Where(predicate);
        }

        public Exposure Get(int id)
        {
            return _context.Exposures.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Exposure> GetAsync(int id)
        {
            return await _context.Exposures.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Exposure item)
        {
            _context.Exposures.Add(item);
        }
        public async Task CreateAsync(Exposure item)
        {
            await _context.Exposures.AddAsync(item);
        }

        public void Update(Exposure item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Exposure item = _context.Exposures.Find(id);
            if (item != null)
                _context.Exposures.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            Exposure item = await _context.Exposures.FindAsync(id);
            if (item != null)
                _context.Exposures.Remove(item);
        }
    }
}
