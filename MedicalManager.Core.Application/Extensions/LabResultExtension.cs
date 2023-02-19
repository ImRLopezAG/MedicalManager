using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class LabResultExtension {
  public static LabResult ConvertToSave(this SaveLabResultVm labResultToSave) => new() {
    Status = "Pending",
    Result = "Not Available",
    CreatedAt = DateTime.Now,
  };

  public static LabResult ConvertToUpdate(this SaveLabResultVm labResultToUpdate, LabResult labResult) {
    labResult.Status = labResultToUpdate.Status;
    labResult.Result = labResultToUpdate.Result;
    labResult.LastModifiedAt = DateTime.Now;
    labResult.LastModifiedBy = labResultToUpdate.LastModifiedBy;
    return labResult;
  }

  public static LabResultVm ConvertToVm(this LabResult labResult, string TName, string PName, string PDni) => new() {
    Id = labResult.Id,
    Status = labResult.Status,
    LabTest = TName,
    Patient = PName,
    Result = labResult.Result,
    DateId = labResult.DateId,
    DNI = PDni,
    CreatedAt = labResult.CreatedAt,
    LastModifiedAt = labResult.LastModifiedAt,
  };

  public static LabResult ConvertToComplete(this LabResult labResult, string modifiedBy, string result) {
    labResult.LastModifiedAt = DateTime.Now;
    labResult.Status = "Completed";
    labResult.Result = result;
    labResult.LastModifiedBy = modifiedBy;
    return labResult;
  }

  public static SaveLabResultVm ConvertToSaveVm(this LabResult labResult) => new() {
    Id = labResult.Id,
    Status = labResult.Status,
  };
}
