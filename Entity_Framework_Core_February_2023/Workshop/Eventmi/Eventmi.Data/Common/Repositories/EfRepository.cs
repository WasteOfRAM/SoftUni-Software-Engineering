using Eventmi.Core;
using Eventmi.Data.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Data.Common.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EfRepository(EventmiDbContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<TEntity>();
        }

        protected DbContext Context { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await DbSet.AddRangeAsync(entities);

        public async Task<TEntity> GetByIdAsync(object Id) => await DbSet.FindAsync(Id);

        public IQueryable<TEntity> All() => DbSet.AsQueryable();

        public IQueryable<TEntity> AllAsNoTracking() => DbSet.AsQueryable().AsNoTracking();

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        public void Update(TEntity entity) => Context.Update(entity);
    }
}
