using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class PatientExtension {
  public static Patient ConvertToEntity(this SavePatientVm patientToSave) => new() {
    FirstName = patientToSave.FirstName,
    LastName = patientToSave.LastName,
    Identification = patientToSave.Identification,
    Email = patientToSave.Email,
    Phone = patientToSave.Identification,
    Photo = patientToSave.Photo,
    BirthDate = patientToSave.BirthDate,
    Smoker = patientToSave.Smoker,
    Allergic = patientToSave.Allergic,
    Address = patientToSave.Address,
    CreatedAt = DateTime.Now,
    CreatedBy = patientToSave.CreatedBy,
  };

  public static Patient ConvertToUpdate(this SavePatientVm updatePatient, Patient patient) {
    patient.FirstName = updatePatient.FirstName;
    patient.LastName = updatePatient.LastName;
    patient.Email = updatePatient.Email;
    patient.Identification = updatePatient.Identification;
    patient.Phone = updatePatient.Phone;
    patient.Photo = updatePatient.Photo;
    patient.Smoker = updatePatient.Smoker;
    patient.Allergic = updatePatient.Allergic;
    patient.Identification = updatePatient.Identification;
    patient.Address = updatePatient.Address;
    patient.BirthDate = updatePatient.BirthDate;
    patient.LastModifiedAt = DateTime.Now;
    patient.LastModifiedBy = updatePatient.LastModifiedBy;
    return patient;
  }
  public static PatientVm ConvertToVm(this Patient patient, string IsAllergic, string IsSmoker) => new() {
    Id = patient.Id,
    FirstName = patient.FirstName,
    LastName = patient.LastName,
    Email = patient.Email,
    Identification = patient.Identification,
    Smoker = IsSmoker,
    Allergic = IsAllergic,
    Photo = patient.Photo,
    BirthDate = patient.BirthDate.ConvertToDate(),
    Address = patient.Address,
    Phone = patient.Phone,
    CreatedAt = patient.CreatedAt,
    LastModifiedAt = patient.LastModifiedAt
  };

  public static SavePatientVm ConvertToSaveVm(this Patient patient) => new() {
    Id = patient.Id,
    FirstName = patient.FirstName,
    LastName = patient.LastName,
    Email = patient.Email,
    Identification = patient.Identification,
    Smoker = patient.Smoker,
    Allergic = patient.Allergic,
    Phone = patient.Phone,
    Photo = patient.Photo,
    BirthDate = patient.BirthDate,
    Address = patient.Address,
    CreatedAt = patient.CreatedAt,
    LastModifiedAt = patient.LastModifiedAt
  };
}
