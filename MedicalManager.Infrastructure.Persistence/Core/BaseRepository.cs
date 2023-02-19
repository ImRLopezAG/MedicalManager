using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Domain.Core;
using MedicalManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalManager.Infrastructure.Persistence.Core;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity {
  private readonly MedicalContext _context;

  public BaseRepository(MedicalContext context) {
    _context = context;
  }
  public virtual async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().OrderByDescending(x => x.CreatedAt).ToListAsync();
  public virtual async Task<TEntity> GetEntity(int id) => await _context.Set<TEntity>().FindAsync(id);
  public virtual async Task<TEntity> GetById(int id) => await _context.Set<TEntity>().FirstOrDefaultAsync(id => id == id);
  public async Task<bool> Exists(Expression<Func<TEntity, bool>> Filter) => await _context.Set<TEntity>().AnyAsync(Filter);

  public virtual async Task<TEntity> Save(TEntity entity) {
    await _context.Set<TEntity>().AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public virtual async Task Update(TEntity entity) {
    _context.Set<TEntity>().Update(entity);
    await _context.SaveChangesAsync();
  }

  public virtual async Task Delete(TEntity entity) {
    _context.Set<TEntity>().Remove(entity);
    await _context.SaveChangesAsync();
  }
}
