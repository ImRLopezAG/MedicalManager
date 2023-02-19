using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Interfaces {
  public interface IDateRepository : IBaseRepository<Date> {
    Task<IEnumerable<Date>> GetByPatientId(int patientId);
  }
}


