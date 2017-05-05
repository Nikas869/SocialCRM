using System.Threading.Tasks;
using SocialCRM.Server.DataAccess.Repositories;

namespace SocialCRM.Server.DataAccess.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(this.context);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return this.context.SaveChangesAsync();
        }
    }
}