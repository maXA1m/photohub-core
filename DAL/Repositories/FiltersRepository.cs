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
    public class FiltersRepository : IRepository<Filter>
    {
        private readonly ApplicationDbContext _context;

        public FiltersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Filter> GetAll()
        {
            return _context.Filters;
        }

        public IEnumerable<Filter> GetAll(int page, int pageSize)
        {
            return _context.Filters.Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Filter> Find(Func<Filter, bool> predicate)
        {
            return _context.Filters.Where(predicate);
        }

        public Filter Get(int id)
        {
            return _context.Filters.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Filter> GetAsync(int id)
        {
            return await _context.Filters.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Filter item)
        {
            _context.Filters.Add(item);
        }
        public async Task CreateAsync(Filter item)
        {
            await _context.Filters.AddAsync(item);
        }

        public void Update(Filter item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Filter filter = _context.Filters.Find(id);
            if (filter != null)
                _context.Filters.Remove(filter);
        }
        public async Task DeleteAsync(int id)
        {
            Filter filter = await _context.Filters.FindAsync(id);
            if (filter != null)
                _context.Filters.Remove(filter);
        }
    }
}
