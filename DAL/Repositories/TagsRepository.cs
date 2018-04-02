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
    public class TagsRepository : IRepository<Tag>
    {
        private readonly ApplicationDbContext _context;

        public TagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.OrderBy(i => i.Id);
        }

        public IEnumerable<Tag> GetAll(int page, int pageSize)
        {
            return _context.Tags.OrderBy(i => i.Id).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return _context.Tags.OrderBy(i => i.Id).Where(predicate);
        }

        public Tag Get(int id)
        {
            return _context.Tags.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Tag> GetAsync(int id)
        {
            return await _context.Tags.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Tag item)
        {
            _context.Tags.Add(item);
        }
        public async Task CreateAsync(Tag item)
        {
            await _context.Tags.AddAsync(item);
        }

        public void Update(Tag item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tag item = _context.Tags.Find(id);
            if (item != null)
                _context.Tags.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            Tag item = await _context.Tags.FindAsync(id);
            if (item != null)
                _context.Tags.Remove(item);
        }
    }
}
