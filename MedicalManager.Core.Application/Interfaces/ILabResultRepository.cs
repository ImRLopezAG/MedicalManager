using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Interfaces;

public interface ILabResultRepository : IBaseRepository<LabResult> {
  Task<IEnumerable<LabResult>> GetByDNI(string dni);
  Task<LabResult> GetByDate(int date);
  Task<IEnumerable<LabResult>> GetByDates(int date);
}
