using BooksApp.Core.Ports.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Core.Adapters.DataAccess;

public class BaseRepository<TContext, TEntity>(TContext dbContext) : IBaseRepository<TEntity>
    where TContext : DbContext
    where TEntity : class, new()
{
    protected readonly TContext dbContext = dbContext;

    public TEntity Get(Guid id)
    {
        return this.dbContext.Set<TEntity>().Find(id) ?? new TEntity();
    }

    public IEnumerable<TEntity> Get()
    {
        return this.dbContext.Set<TEntity>();
    }

    public TEntity Add(TEntity entity)
    {
        this.dbContext.Set<TEntity>().Add(entity);
        this.dbContext.SaveChanges();
        return entity;
    }
}
