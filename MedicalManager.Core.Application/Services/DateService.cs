using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Services;

public class DateService : IDateService {
  private readonly IDateRepository _dateRepository;
  private readonly IPatientRepository _patientRepository;
  private readonly IDoctorRepository _doctorRepository;
  private readonly ILabResultRepository _labResultRepository;
  private readonly ILabTestRepository _labTestRepository;
  private readonly ILoggerService<DateService> _loggerService;

  public DateService(IDateRepository dateRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, ILabResultRepository labResultRepository, ILabTestRepository labTestRepository, ILoggerService<DateService> loggerService) {
    _dateRepository = dateRepository;
    _patientRepository = patientRepository;
    _doctorRepository = doctorRepository;
    _labResultRepository = labResultRepository;
    _labTestRepository = labTestRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var date = await _dateRepository.GetEntity(id);
      if (date != null) {
        result.Data = date.ConvertToVm(
          _patientRepository.GetEntity(date.PatientId).ContinueWith(t => t.Result.FirstName + " " + t.Result.LastName).Result,
          _doctorRepository.GetEntity(date.DoctorId).ContinueWith(t => t.Result.FirstName + " " + t.Result.LastName).Result
        );
      } else {
        result.Success = false;
        result.Message = "Date not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting date";
    }
    return result;
  }

  public async Task<SaveDateVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var date = await _dateRepository.GetEntity(id);
      if (date != null) {
        return date.ConvertToSaveVm(
          _doctorRepository.GetAll().ContinueWith(t => t.Result.Select(d => d.ConvertToVm()).ToList()).Result,
          _patientRepository.GetAll().ContinueWith(t => t.Result.Select(p => new PatientVm { Id = p.Id, FirstName = p.FirstName + " " + p.LastName }).ToList()).Result,
          _labTestRepository.GetAll().ContinueWith(t => t.Result.Select(l => l.ConvertToVm()).ToList()).Result
        );
      } else {
        result.Success = false;
        result.Message = "Date not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting date";
    }
    return null;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from date in await _dateRepository.GetAll()
                  select date.ConvertToVm(
                    _patientRepository.GetEntity(date.PatientId).ContinueWith(t => t.Result.FirstName + " " + t.Result.LastName).Result,
                    _doctorRepository.GetEntity(date.DoctorId).ContinueWith(t => t.Result.FirstName + " " + t.Result.LastName).Result
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting dates";
    }
    return result;
  }

  public async Task AddTest(int DateId, int[] TestId, string createdBy) {
    ServiceResult result = new();
    try {
      var date = await _dateRepository.GetEntity(DateId);
      if (date != null) {
        foreach (var testId in TestId) {
          var test = await _labTestRepository.GetEntity(testId);
          if (test != null) {
            var resultToSave = new LabResult {
              LabTestId = test.Id,
              Status = "Pending",
              DateId = DateId,
              Result = "Not available",
              CreatedBy = createdBy,
              CreatedAt = DateTime.Now
            };
            await _labResultRepository.Save(resultToSave);
          } else {
            result.Success = false;
            result.Message = "Test not found";
          }
        }
        var updateDate = date.AddTest();
        await _dateRepository.Update(updateDate);
      } else {
        result.Success = false;
        result.Message = "Date not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while adding test";
    }
  }
  public async Task<SaveDateVm> Save(SaveDateVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidDate = ValidateDate.IsValidDate(vm);
      if (isValidDate.Success) {
        vm.CreatedBy = createdBy;
        Date dateToSave = vm.ConvertToSave();
        var newDate = await _dateRepository.Save(dateToSave);
        return newDate.ConvertToSaveVm(
          _doctorRepository.GetAll().ContinueWith(t => t.Result.Select(d => d.ConvertToVm()).ToList()).Result,
          _patientRepository.GetAll().ContinueWith(t => t.Result.Select(p => new PatientVm { Id = p.Id, FirstName = p.FirstName + " " + p.LastName }).ToList()).Result,
          _labTestRepository.GetAll().ContinueWith(t => t.Result.Select(l => l.ConvertToVm()).ToList()).Result
        );
      } else {
        result.Success = false;
        result.Message = isValidDate.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving date";

    }
    return null;
  }

  public async Task Edit(SaveDateVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidDate = ValidateDate.IsValidDate(vm);
      if (isValidDate.Success) {
        var date = await _dateRepository.GetEntity(vm.Id);
        if (date != null) {
          date.LastModifiedBy = modifiedBy;
          Date dateToUpdate = vm.ConvertToUpdate(date);
          await _dateRepository.Update(dateToUpdate);
        } else {
          result.Success = false;
          result.Message = "Date not found";
        }
      } else {
        result.Success = false;
        result.Message = isValidDate.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while editing date";
    }
  }
  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      var date = await _dateRepository.GetEntity(id);
      if (date != null) {
        await _dateRepository.Delete(date);
      } else {
        result.Success = false;
        result.Message = "Date not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while deleting date";
    }
  }

}
