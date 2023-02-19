using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class LabTestExtension {
  public static LabTest ConvertToSave(this SaveLabTestVm labTestToSave) => new() {
    Name = labTestToSave.Name,
    CreatedAt = DateTime.Now,
    CreatedBy = labTestToSave.CreatedBy,
  };

  public static LabTest ConvertToUpdate(this SaveLabTestVm labTestToUpdate, LabTest labTest) {
    labTest.Name = labTestToUpdate.Name;
    labTest.LastModifiedAt = DateTime.Now;
    labTest.LastModifiedBy = labTestToUpdate.LastModifiedBy;
    return labTest;
  }

  public static LabTestVm ConvertToVm(this LabTest labTest) => new() {
    Id = labTest.Id,
    Name = labTest.Name,
  };

  public static SaveLabTestVm ConvertToSaveVm(this LabTest labTest) => new() {
    Id = labTest.Id,
    Name = labTest.Name
  };
}
