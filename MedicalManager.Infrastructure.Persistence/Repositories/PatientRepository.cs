using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository {
  private readonly MedicalContext _context;

  public PatientRepository(MedicalContext context) : base(context) => _context = context;
  public async Task<Patient> GetByDNI(string dni) => await _context.Patients.FirstOrDefaultAsync(x => x.Identification == dni);

}
