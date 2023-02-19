using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Validations;

public static class ValidateDate {
  public static ServiceResult IsValidDate(SaveDateVm date) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(date.Day.ToString())) {
      result.Message = "Day is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(date.Hour.ToString())) {
      result.Message = "Hour is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(date.PatientId.ToString())) {
      result.Message = "Patient is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(date.DoctorId.ToString())) {
      result.Message = "Doctor is required";
      result.Success = false;
    }
    return result;
  }
}
