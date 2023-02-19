using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class DateRepository : BaseRepository<Date>, IDateRepository {
  private readonly DbSet<Date> _dbSet;

  public DateRepository(MedicalContext context) : base(context) {
    _dbSet = context.Set<Date>();
  }
  public async Task<IEnumerable<Date>> GetByPatientId(int patientId) => await _dbSet.Where(x => x.PatientId == patientId).ToListAsync();

}
