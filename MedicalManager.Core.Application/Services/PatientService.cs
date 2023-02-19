using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels.SaveVm;

namespace MedicalManager.Core.Application.Services;

public class PatientService : IPatientService {

  private readonly IPatientRepository _patientRepository;
  private readonly ILoggerService<PatientService> _loggerService;

  public PatientService(IPatientRepository patientRepository, ILoggerService<PatientService> loggerService) {
    _patientRepository = patientRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var patient = await _patientRepository.GetById(id);
      if (patient == null) {
        result.Message = "Patient not found";
        result.Success = false;
        return result;
      }
      result.Data = patient.ConvertToVm(
        patient.Allergic ? "Yes" : "No",
        patient.Smoker ? "Yes" : "No"
      );
    } catch (Exception e) {
      _loggerService.LogError(e.Message);
      result.Message = "An error occurred while getting patient";
      result.Success = false;
    }
    return result;
  }

  public async Task<SavePatientVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var patient = await _patientRepository.GetEntity(id);
      if (patient != null) {
        return patient.ConvertToSaveVm();
      } else {
        result.Message = "Patient not found";
        result.Success = false;
      }
    } catch (Exception e) {
      _loggerService.LogError(e.Message);
      result.Message = "An error occurred while getting patient";
      result.Success = false;
    }
    return null;
  }

  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from patient in await _patientRepository.GetAll()
                  select patient.ConvertToVm(
                    patient.Allergic ? "Yes" : "No",
                    patient.Smoker ? "Yes" : "No");
      result.Data = query.ToList();
    } catch (Exception e) {
      _loggerService.LogError(e.Message);
      result.Message = "An error occurred while getting patients";
      result.Success = false;
    }
    return result;
  }
  public async Task<SavePatientVm> Save(SavePatientVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidPatient = ValidatePatient.IsValidPatient(vm);
      if (isValidPatient.Success) {
        vm.CreatedBy = createdBy;
        var patient = vm.ConvertToEntity();
        var newPatient = await _patientRepository.Save(patient);
        return newPatient.ConvertToSaveVm();
      } else {
        result.Message = isValidPatient.Message;
        result.Success = false;
      }
    } catch (Exception e) {
      result.Message = "An error occurred while saving patient";
      result.Success = false;
      _loggerService.LogError(e.Message);
    }
    return null;
  }
  public async Task Edit(SavePatientVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidPatient = ValidatePatient.IsValidPatient(vm);
      if (isValidPatient.Success) {
        var patientToUpdate = await _patientRepository.GetEntity(vm.Id);
        if (patientToUpdate != null) {
          vm.LastModifiedBy = modifiedBy;
          patientToUpdate = vm.ConvertToUpdate(patientToUpdate);
          await _patientRepository.Update(patientToUpdate);
        } else {
          result.Message = "Patient not found";
          result.Success = false;
        }

      } else {
        result.Message = isValidPatient.Message;
        result.Success = false;
      }
    } catch (Exception e) {
      result.Message = "An error occurred while saving patient";
      result.Success = false;
      _loggerService.LogError(e.Message);
    }
  }
  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      var patientToDelete = await _patientRepository.GetEntity(id);
      if (patientToDelete == null) {
        result.Message = "Patient not found";
        result.Success = false;
        return;
      }
      await _patientRepository.Delete(patientToDelete);
    } catch (Exception e) {
      result.Message = "An error occurred while deleting patient";
      result.Success = false;
      _loggerService.LogError(e.Message);
    }
  }
}
