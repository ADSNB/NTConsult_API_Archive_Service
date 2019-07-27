using System.Linq;
using System.Threading.Tasks;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    TEntity GetById(int id);

    TEntity Create(TEntity entity);

    void Update(int id, TEntity entity);

    void Delete(int id);
}