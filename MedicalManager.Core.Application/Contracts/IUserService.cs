using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Contracts;

public interface IUserService : IGenericService<UserVm, SaveUserVm> {
  Task<UserVm> Login(LoginVm vm);
  Task<bool> UserExists(string UserName);
}
