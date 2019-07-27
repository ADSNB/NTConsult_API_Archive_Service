using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq;
using System.Threading.Tasks;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly QueueContext _dbContext;

    public GenericRepository(QueueContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }

    public TEntity GetById(int id)
    {
        return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
    }

    public TEntity Create(TEntity entity)
    {
        var response = _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
        return response.Entity;
    }

    public void Update(int id, TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.SaveChanges();
    }
}