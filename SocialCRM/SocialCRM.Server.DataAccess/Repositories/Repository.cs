using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialCRM.Server.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(ApplicationDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        protected ApplicationDbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return this.DbSet.FindAsync(id);
        }

        public virtual int Count(Func<TEntity, bool> filter = null)
        {
            return filter == null
                ? this.DbSet.Count()
                : this.DbSet.Count(filter);
        }

        public virtual Task<int> CountAsync()
        {
            return this.DbSet.CountAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.DbSet.Attach(entityToUpdate);
            this.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = this.DbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }

            this.DbSet.Remove(entityToDelete);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}