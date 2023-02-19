using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Validations;

public static class ValidateDoctor {
  public static ServiceResult IsValidDoctor(SaveDoctorVm doctor) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(doctor.FirstName)) {
      result.Message = "Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(doctor.LastName)) {
      result.Message = "Last Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(doctor.Email)) {
      result.Message = "Email is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(doctor.Phone)) {
      result.Message = "Phone is required";
      result.Success = false;
    }
    return result;
  }
}
