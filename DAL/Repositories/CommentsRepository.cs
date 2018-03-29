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
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll(int page, int pageSize)
        {
            return _context.Comments.Include(c => c.Owner).OrderBy(c => c.Date).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return _context.Comments.Include(c => c.Owner).Where(predicate);
        }

        public Comment Get(int id)
        {
            return _context.Comments.Include(c => c.Owner).Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Comment> GetAsync(int id)
        {
            return await _context.Comments.Include(c => c.Owner).Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }
        public async Task CreateAsync(Comment item)
        {
            await _context.Comments.AddAsync(item);
        }

        public void Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Comment comment = _context.Comments.Find(id);
            if (comment != null)
                _context.Comments.Remove(comment);
        }
        public async Task DeleteAsync(int id)
        {
            Comment comment = await _context.Comments.FindAsync(id);
            if (comment != null)
                _context.Comments.Remove(comment);
        }
    }
}