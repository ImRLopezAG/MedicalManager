using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class UserExtension {
  public static User ConvertToSave(this SaveUserVm userToSave) => new() {
    FirstName = userToSave.FirstName,
    LastName = userToSave.LastName,
    Email = userToSave.Email,
    UserName = userToSave.UserName,
    Password = EncryptPassword.Encrypt(userToSave.Password),
    Role = userToSave.Role,
    CreatedAt = DateTime.Now,
    CreatedBy = userToSave.CreatedBy,
  };

  public static User ConvertToUpdate(this SaveUserVm updateUser, User user) {
    user.FirstName = updateUser.FirstName;
    user.LastName = updateUser.LastName;
    user.Email = updateUser.Email;
    user.UserName = updateUser.UserName;
    user.Password = EncryptPassword.Encrypt(updateUser.Password);
    user.Role = updateUser.Role;
    user.LastModifiedAt = DateTime.Now;
    user.LastModifiedBy = updateUser.LastModifiedBy;
    return user;
  }
  public static UserVm ConvertToVm(this User user) => new() {
    Id = user.Id,
    FirstName = user.FirstName,
    LastName = user.LastName,
    UserName = user.UserName,
    Email = user.Email,
    Role = user.Role,
    CreatedAt = user.CreatedAt,
    LastModifiedAt = user.LastModifiedAt
  };

  public static SaveUserVm ConvertToSaveVm(this User user) => new() {
    Id = user.Id,
    FirstName = user.FirstName,
    LastName = user.LastName,
    UserName = user.UserName,
    Email = user.Email,
    Role = user.Role,
    Password = EncryptPassword.Encrypt(user.Password),
    CreatedAt = user.CreatedAt,
    LastModifiedAt = user.LastModifiedAt
  };
}
