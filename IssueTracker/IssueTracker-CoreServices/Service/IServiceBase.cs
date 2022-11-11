using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker_CoreServices.Services
{
    public class IServiceBase<TContext, TEntity, TComparable>
        where TContext : DbContext
        where TEntity : class
        where TComparable : IComparable
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        public IServiceBase(TContext context, DbSet<TEntity> set)
        {
            _context = context;
            _set = set;
        }
        public async Task AddAsync(TEntity entity) => await _set.AddAsync(entity);

#nullable enable
        public async Task<TEntity?> FindAsync(TComparable id) => await _set.FindAsync(id);
#nullable disable
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _set.ToListAsync();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public bool Exists(TComparable id) => FindAsync(id).Result != null;
        public void Remove(TEntity entity) => _set.Remove(entity);
        public void Update(TEntity entity) => _set.Update(entity);

        public IIncludableQueryable<TEntity, TProperty> QueryWithInclude<TProperty>(Expression<Func<TEntity, TProperty>> navigation) => _set.Include(navigation);
    }
}

