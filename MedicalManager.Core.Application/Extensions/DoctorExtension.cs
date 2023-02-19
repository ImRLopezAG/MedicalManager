using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Extensions;

public static class DoctorExtension {
  public static Doctor ConvertToSave(this SaveDoctorVm doctorToSave) => new() {
    FirstName = doctorToSave.FirstName,
    LastName = doctorToSave.LastName,
    Identification = doctorToSave.Identification,
    Email = doctorToSave.Email,
    Phone = doctorToSave.Phone,
    Photo = doctorToSave.Photo,
    CreatedAt = DateTime.Now,
    CreatedBy = doctorToSave.CreatedBy,
  };

  public static Doctor ConvertToUpdate(this SaveDoctorVm doctorToUpdate, Doctor doctor) {
    doctor.FirstName = doctorToUpdate.FirstName;
    doctor.LastName = doctorToUpdate.LastName;
    doctor.Identification = doctorToUpdate.Identification;
    doctor.Email = doctorToUpdate.Email;
    doctor.Phone = doctorToUpdate.Phone;
    doctor.Photo = doctorToUpdate.Photo;
    doctor.LastModifiedAt = DateTime.Now;
    doctor.LastModifiedBy = doctorToUpdate.LastModifiedBy;
    return doctor;
  }



  public static DoctorVm ConvertToVm(this Doctor doctor) => new() {
    Id = doctor.Id,
    FirstName = doctor.FirstName,
    LastName = doctor.LastName,
    Identification = doctor.Identification,
    Photo = doctor.Photo,
    Email = doctor.Email,
    Phone = doctor.Phone,
  };

  public static SaveDoctorVm ConvertToSaveVm(this Doctor doctor) => new() {
    Id = doctor.Id,
    FirstName = doctor.FirstName,
    LastName = doctor.LastName,
    Identification = doctor.Identification,
    Photo = doctor.Photo,
    Email = doctor.Email,
    Phone = doctor.Phone,
  };
}
