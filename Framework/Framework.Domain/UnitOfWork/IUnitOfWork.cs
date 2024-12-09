using Framework.Domain.Entities;
using Framework.Domain.Repositories;

namespace Framework.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : IBaseEntity;
    Task<int> SaveChangesAsync();
}