using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Validations;

public static class ValidatePatient {
  public static ServiceResult IsValidPatient(SavePatientVm patient) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(patient.FirstName)) {
      result.Message = "Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(patient.LastName)) {
      result.Message = "Last Name is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(patient.Email)) {
      result.Message = "Email is required";
      result.Success = false;
    }
    if (string.IsNullOrEmpty(patient.Phone)) {
      result.Message = "Phone is required";
      result.Success = false;
    }
    if (patient.BirthDate == null) {
      result.Message = "Birth Date is required";
      result.Success = false;
    }
    if (patient.BirthDate > DateTime.Now) {
      result.Message = "Birth Date must be less than today";
      result.Success = false;
    }
    if (patient.BirthDate < DateTime.Now.AddYears(-100)) {
      result.Message = "Birth Date must be greater than 100 years ago";
      result.Success = false;
    }
    return result;
  }
}
