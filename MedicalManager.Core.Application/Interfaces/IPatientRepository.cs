using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient> {
  Task<Patient> GetByDNI(string dni);
}
