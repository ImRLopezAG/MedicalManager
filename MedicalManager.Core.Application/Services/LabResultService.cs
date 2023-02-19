using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Services;

public class LabResultService : ILabResultService {

  private readonly ILabResultRepository _labResultRepository;
  private readonly IDateRepository _dateRepository;
  private readonly ILabTestRepository _labTestRepository;
  private readonly IPatientRepository _patientRepository;
  private readonly ILoggerService<LabResultService> _loggerService;

  public LabResultService(ILabResultRepository labResultRepository, IDateRepository dateRepository, ILabTestRepository labTestRepository, IPatientRepository patientRepository, ILoggerService<LabResultService> loggerService) {
    _labResultRepository = labResultRepository;
    _dateRepository = dateRepository;
    _labTestRepository = labTestRepository;
    _patientRepository = patientRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var labResult = await _labResultRepository.GetEntity(id);
      var test = await _labTestRepository.GetEntity(labResult.LabTestId);
      var date = await _dateRepository.GetEntity(labResult.DateId);
      var patient = await _patientRepository.GetEntity(date.PatientId);
      if (labResult != null) {
        result.Data = labResult.ConvertToVm(
          test.Name,
          patient.FirstName + " " + patient.LastName,
          patient.Identification
          );
      } else {
        result.Success = false;
        result.Message = "Lab Result not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Result";
    }
    return result;
  }

  public async Task<SaveLabResultVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var labResult = await _labResultRepository.GetEntity(id);
      if (labResult != null) {
        return labResult.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = "Lab Result not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Result";
    }
    return null;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from labResult in await _labResultRepository.GetAll()
                  select labResult.ConvertToVm(
                  _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Results";
    }
    return result;
  }


  public async Task<ServiceResult> GetByDNI(string dni) {
    ServiceResult result = new();
    try {
      if (_patientRepository.GetByDNI(dni).Result != null) {
        var query = from labResult in await _labResultRepository.GetByDNI(dni)
                    select labResult.ConvertToVm(
                    _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                    );
        result.Data = query.ToList();
      } else {
        result.Success = false;
        result.Message = "Patient not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = $"Error while getting the Lab Results of the Patient{dni}";
    }
    return result;
  }

  public async Task<ServiceResult> GetByDate(int DateId) {
    ServiceResult result = new();
    try {
      var query = from labResult in await _labResultRepository.GetByDates(DateId)
                  select labResult.ConvertToVm(
                  _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Results";
    }
    return result;
  }

  public async Task ChangeStatus(int dateId, string modifyBy) {
    ServiceResult result = new();
    try {
      var date = await _dateRepository.GetEntity(dateId);
      date.LastModifiedBy = modifyBy;
      Date dateToChange = date.ConvertToComplete(modifyBy);
      await _dateRepository.Update(dateToChange);
      var labResults = await _labResultRepository.GetByDate(dateId);
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while changing status";
    }
  }

  public async Task<ServiceResult> GetPending() {
    ServiceResult result = new();
    try {
      var query = from labResult in await _labResultRepository.GetAll()
                  where labResult.Status == "Pending"
                  select labResult.ConvertToVm(
                  _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                  _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Results";
    }
    return result;
  }

  public async Task<ServiceResult> GetByStatus(string status, int dateId) {
    ServiceResult result = new();
    try {
      if (dateId == 0) {
        var query = from labResult in await _labResultRepository.GetAll()
                    where labResult.Status == status
                    select labResult.ConvertToVm(
                    _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                    );
        result.Data = query.ToList();
      } else {
        var query = from labResult in await _labResultRepository.GetByDates(dateId)
                    where labResult.Status == status
                    select labResult.ConvertToVm(
                    _labTestRepository.GetEntity(labResult.LabTestId).Result.Name,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.FirstName + " " + _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.LastName,
                    _patientRepository.GetEntity(_dateRepository.GetEntity(labResult.DateId).Result.PatientId).Result.Identification
                    );
        result.Data = query.ToList();
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting Lab Results";
    }
    return result;
  }
  public async Task<SaveLabResultVm> Save(SaveLabResultVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidLabResult = ValidateLabResult.IsValidLabResult(vm);
      if (isValidLabResult.Success) {
        vm.CreatedBy = createdBy;
        LabResult labResult = vm.ConvertToSave();
        var newResult = await _labResultRepository.Save(labResult);
        return newResult.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = isValidLabResult.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving Lab Result";
    }
    return null;
  }

  public async Task Edit(SaveLabResultVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidLabResult = ValidateLabResult.IsValidLabResult(vm);
      if (isValidLabResult.Success) {
        var resultToUpdate = await _labResultRepository.GetEntity(vm.Id);
        if (resultToUpdate != null) {
          LabResult labResult = resultToUpdate.ConvertToComplete(modifiedBy, vm.Result);
          await _labResultRepository.Update(labResult);
        } else {
          result.Success = false;
          result.Message = "Lab Result not found";
        }
      } else {
        result.Success = false;
        result.Message = isValidLabResult.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while editing Lab Result";
    }

  }

  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      var labResult = await _labResultRepository.GetEntity(id);
      if (labResult != null) {
        await _labResultRepository.Delete(labResult);
      } else {
        result.Success = false;
        result.Message = "Lab Result not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while deleting Lab Result";
    }
  }
}
