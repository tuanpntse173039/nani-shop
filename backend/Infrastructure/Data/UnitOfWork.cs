using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _context;
    private Hashtable? _repositories;

    public UnitOfWork(StoreContext context)
    {
        _context = context;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var entityType = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)),
                _context
            );
            _repositories.Add(entityType, repositoryInstance);
        }

        return (IGenericRepository<TEntity>?)_repositories[entityType];
    }
}
