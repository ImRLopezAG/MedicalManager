using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Contracts;

public interface ILabResultService : IGenericService<LabResultVm, SaveLabResultVm> {
  Task<ServiceResult> GetByDNI(string dni);
  Task<ServiceResult> GetByDate(int DateId);
  Task<ServiceResult> GetByStatus(string status, int dateId = 0);
  Task ChangeStatus(int dateIdm, string modifyBy);
}
