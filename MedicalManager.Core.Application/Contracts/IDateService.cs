using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Contracts;

public interface IDateService : IGenericService<DateVm, SaveDateVm> {
  Task AddTest(int DateId, int[] TestId, string createdBy);
}
