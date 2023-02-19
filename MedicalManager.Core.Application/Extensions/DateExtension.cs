using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class DateExtension {
  public static Date ConvertToSave(this SaveDateVm dateToSave) => new() {
    Description = dateToSave.Description,
    Day = dateToSave.Day,
    Hour = dateToSave.Hour,
    Status = "Pending",
    DoctorId = dateToSave.DoctorId,
    PatientId = dateToSave.PatientId,
    CreatedAt = DateTime.Now,
    CreatedBy = dateToSave.CreatedBy,
  };

  public static Date ConvertToUpdate(this SaveDateVm updateDate, Date date) {
    date.Description = updateDate.Description;
    date.Day = updateDate.Day;
    date.Hour = updateDate.Hour;
    date.Status = updateDate.Status;
    date.DoctorId = updateDate.DoctorId;
    date.PatientId = updateDate.PatientId;
    date.LastModifiedBy = updateDate.LastModifiedBy;
    date.LastModifiedAt = DateTime.Now;
    return date;
  }

  public static Date AddTest(this Date date) {
    date.LastModifiedAt = DateTime.Now;
    date.Status = "On Hold";
    return date;
  }

  public static Date ConvertToComplete(this Date date, string modifiedBy) {
    date.LastModifiedAt = DateTime.Now;
    date.Status = "Completed";
    date.LastModifiedBy = modifiedBy;
    return date;
  }



  public static DateVm ConvertToVm(this Date date, string PtName, string DcName) => new() {
    Id = date.Id,
    Description = date.Description,
    Day = date.Day.ConvertToDate(),
    Hour = date.Hour.ConvertToTime(),
    Status = date.Status,
    Doctor = DcName,
    Patient = PtName,
    CreatedAt = date.CreatedAt,
    LastModifiedAt = date.LastModifiedAt
  };

  public static SaveDateVm ConvertToSaveVm(this Date date, List<DoctorVm> Dcs, List<PatientVm> Pts, List<LabTestVm> tests) => new() {
    Id = date.Id,
    Description = date.Description,
    Day = date.Day,
    Hour = date.Hour,
    Status = date.Status,
    DoctorId = date.DoctorId,
    PatientId = date.PatientId,
    Doctors = Dcs,
    Patients = Pts,
    LabTests = tests,
    CreatedAt = date.CreatedAt,
    LastModifiedAt = date.LastModifiedAt
  };
}
