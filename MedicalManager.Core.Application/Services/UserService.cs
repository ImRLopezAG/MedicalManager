using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Services;

public class UserService : IUserService {
  private readonly IUserRepository _userRepository;
  private readonly ILoggerService<UserService> _loggerService;

  public UserService(IUserRepository userRepository, ILoggerService<UserService> loggerService) {
    _userRepository = userRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var user = await _userRepository.GetEntity(id);
      if (user != null) {
        result.Data = user.ConvertToVm();
      } else {
        result.Success = false;
        result.Message = "User not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting user";
    }
    return result;
  }
  public async Task<SaveUserVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var user = await _userRepository.GetEntity(id);
      if (user != null) {
        return user.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = "User not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting user";
    }
    return null;
  }

  public async Task<bool> UserExists(string UserName) {
    return await _userRepository.Exists(us => us.UserName == UserName);
  }
  public async Task<UserVm> Login(LoginVm vm) {
    ServiceResult result = new();
    try {
      var user = await _userRepository.Login(vm);
      if (user != null) {
        return user.ConvertToVm();
      } else {
        result.Success = false;
        result.Message = "User not found";
      }

    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting user";
    }
    return null;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from user in await _userRepository.GetAll()
                  select user.ConvertToVm();
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting users";
    }
    return result;
  }
  public async Task<SaveUserVm> Save(SaveUserVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidUser = ValidateUser.IsValidUser(vm);
      if (isValidUser.Success) {
        vm.CreatedBy = createdBy;
        var user = vm.ConvertToSave();
        var newUser = await _userRepository.Save(user);
        return newUser.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = isValidUser.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving user";
    }
    return null;
  }
  public async Task Edit(SaveUserVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidUser = ValidateUser.IsValidUser(vm);
      if (isValidUser.Success) {
        var userToUpdate = await _userRepository.GetEntity(vm.Id);
        if (userToUpdate != null) {
          userToUpdate.LastModifiedBy = modifiedBy;
          User user = vm.ConvertToUpdate(userToUpdate);
          await _userRepository.Update(user);
        } else {
          result.Success = false;
          result.Message = "User not found";
        }
      } else {
        result.Success = false;
        result.Message = isValidUser.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving user";
    }
  }

  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      User userToDelete = await _userRepository.GetEntity(id);
      if (userToDelete != null) {
        await _userRepository.Delete(userToDelete);
      } else {
        result.Success = false;
        result.Message = "User not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while deleting user";
    }
  }

}
