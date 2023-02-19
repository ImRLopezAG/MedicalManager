using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository {
  public DoctorRepository(MedicalContext context) : base(context) { }
}
