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
    public class ConfirmationsRepository : IRepository<Confirmed>
    {
        private readonly ApplicationDbContext _context;

        public ConfirmationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Confirmed> GetAll(int page, int pageSize)
        {
            return _context.Confirmed
                            .Include(c => c.Admin)
                            .Include(c => c.User)
                            .Skip(page * pageSize).Take(pageSize);
        }

        public Confirmed Get(int id)
        {
            return _context.Confirmed
                    .Include(c => c.Admin)
                    .Include(c => c.User)
                    .Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Confirmed> GetAsync(int id)
        {
            return await _context.Confirmed
                            .Include(c => c.Admin)
                            .Include(c => c.User)
                            .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Confirmed> Find(Func<Confirmed, bool> predicate)
        {
            return _context.Confirmed.Where(predicate);
        }

        public void Create(Confirmed item)
        {
            _context.Confirmed.Add(item);
        }
        public async Task CreateAsync(Confirmed item)
        {
            await _context.Confirmed.AddAsync(item);
        }

        public void Update(Confirmed item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Confirmed confirmation = _context.Confirmed.Find(id);
            if (confirmation != null)
                _context.Confirmed.Remove(confirmation);
        }
        public async Task DeleteAsync(int id)
        {
            Confirmed confirmation = await _context.Confirmed.FindAsync(id);
            if (confirmation != null)
                _context.Confirmed.Remove(confirmation);
        }
    }
}