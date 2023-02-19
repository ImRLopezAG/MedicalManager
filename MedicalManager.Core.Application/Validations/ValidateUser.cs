using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Validations;

public static class ValidateUser {
  public static ServiceResult IsValidUser(SaveUserVm user) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(user.FirstName)) {
      result.Message = "Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(user.LastName)) {
      result.Message = "Last Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(user.Email)) {
      result.Message = "Email is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(user.Password)) {
      result.Message = "Password is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(user.ConfirmPassword)) {
      result.Message = "Confirm Password is required";
      result.Success = false;
    }
    if (user.Password != user.ConfirmPassword) {
      result.Message = "Password and Confirm Password must be the same";
      result.Success = false;
    }
    if (user.Role != "Admin" && user.Role != "Assistant") {
      result.Message = "Role must be Admin or User";
      result.Success = false;
    }
    return result;
  }
}
