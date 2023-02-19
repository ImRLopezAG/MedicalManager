using System.Linq.Expressions;

namespace MedicalManager.Core.Application.Core;

public interface IBaseRepository<TEntity> where TEntity : class {
  Task<IEnumerable<TEntity>> GetAll();
  Task<TEntity> GetEntity(int id);
  Task<TEntity> GetById(int id);
  Task<bool> Exists(Expression<Func<TEntity, bool>> Filter);
  Task<TEntity> Save(TEntity entity);
  Task Update(TEntity entity);
  Task Delete(TEntity entity);


}
