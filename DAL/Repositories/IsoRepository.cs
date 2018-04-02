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
    public class IsoRepository : IRepository<ISO>
    {
        private readonly ApplicationDbContext _context;

        public IsoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ISO> GetAll()
        {
            return _context.Isos.OrderBy(i => i.Id);
        }

        public IEnumerable<ISO> GetAll(int page, int pageSize)
        {
            return _context.Isos.OrderBy(i => i.Id).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ISO> Find(Func<ISO, bool> predicate)
        {
            return _context.Isos.OrderBy(i => i.Id).Where(predicate);
        }

        public ISO Get(int id)
        {
            return _context.Isos.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<ISO> GetAsync(int id)
        {
            return await _context.Isos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(ISO item)
        {
            _context.Isos.Add(item);
        }
        public async Task CreateAsync(ISO item)
        {
            await _context.Isos.AddAsync(item);
        }

        public void Update(ISO item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ISO item = _context.Isos.Find(id);
            if (item != null)
                _context.Isos.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            ISO item = await _context.Isos.FindAsync(id);
            if (item != null)
                _context.Isos.Remove(item);
        }
    }
}
