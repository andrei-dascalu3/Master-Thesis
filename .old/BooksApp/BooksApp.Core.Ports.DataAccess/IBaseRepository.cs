namespace BooksApp.Core.Ports.DataAccess;

public interface IBaseRepository<TEntity>
    where TEntity : class, new()

{
    TEntity Get(Guid id);
    IEnumerable<TEntity> Get();
    TEntity Add(TEntity entity);
}
