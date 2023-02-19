using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Interfaces;

public interface IUserRepository : IBaseRepository<User> {
  Task<User> Login(LoginVm login);
}
