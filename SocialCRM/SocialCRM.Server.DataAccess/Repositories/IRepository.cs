using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialCRM.Server.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Count(Func<TEntity, bool> filter = null);
        Task<int> CountAsync();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity entity);
        void Save();
        Task SaveAsync();
        void Update(TEntity entityToUpdate);
    }
}