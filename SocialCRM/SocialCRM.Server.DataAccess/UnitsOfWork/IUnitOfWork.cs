using System.Threading.Tasks;
using SocialCRM.Server.DataAccess.Repositories;

namespace SocialCRM.Server.DataAccess.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
        Task SaveAsync();
    }
}