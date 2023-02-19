using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class LabTestRepository : BaseRepository<LabTest>, ILabTestRepository {
  public LabTestRepository(MedicalContext context) : base(context) { }
}
