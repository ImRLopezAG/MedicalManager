using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Services;

public class DoctorService : IDoctorService {

  private readonly IDoctorRepository _doctorRepository;
  private readonly ILoggerService<DoctorService> _loggerService;

  public DoctorService(IDoctorRepository doctorRepository, ILoggerService<DoctorService> loggerService) {
    _doctorRepository = doctorRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var doctor = await _doctorRepository.GetEntity(id);
      if (doctor != null) {
        result.Data = doctor.ConvertToVm();
      } else {
        result.Success = false;
        result.Message = "Doctor not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting doctor";
    }
    return result;
  }

  public async Task<SaveDoctorVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var doctor = await _doctorRepository.GetEntity(id);
      if (doctor != null) {
        return doctor.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = "Doctor not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting doctor";
    }
    return null;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from doctor in await _doctorRepository.GetAll()
                  select doctor.ConvertToVm();
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting doctors";
    }
    return result;
  }
  public async Task<SaveDoctorVm> Save(SaveDoctorVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidDoc = ValidateDoctor.IsValidDoctor(vm);
      if (isValidDoc.Success) {
        vm.CreatedBy = createdBy;
        Doctor doctorToSave = vm.ConvertToSave();
        var newDoctor = await _doctorRepository.Save(doctorToSave);
        return newDoctor.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = isValidDoc.Message;
      }

    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving doctor";
    }
    return null;
  }

  public async Task Edit(SaveDoctorVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidDoc = ValidateDoctor.IsValidDoctor(vm);
      if (isValidDoc.Success) {
        var doctor = await _doctorRepository.GetEntity(vm.Id);
        if (doctor != null) {
          doctor.LastModifiedBy = modifiedBy;
          Doctor doctorToUpdate = vm.ConvertToUpdate(doctor);
          await _doctorRepository.Update(doctorToUpdate);
        } else {
          result.Success = false;
          result.Message = "Doctor not found";
        }
      } else {
        result.Success = false;
        result.Message = isValidDoc.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while editing doctor";
    }
  }
  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      var doctor = await _doctorRepository.GetEntity(id);
      await _doctorRepository.Delete(doctor);
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while deleting doctor";
    }
  }
}
