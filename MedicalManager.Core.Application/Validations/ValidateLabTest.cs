using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Validations;

public static class ValidateLabTest {
  public static ServiceResult IsValidLabTest(SaveLabTestVm labTest) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(labTest.Name)) {
      result.Message = "Name is required";
      result.Success = false;
    }
    return result;
  }
}
