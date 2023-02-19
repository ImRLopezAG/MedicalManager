using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Extensions;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.Validations;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Core.Domain.Entities;

namespace MedicalManager.Core.Application.Services;

public class LabTestService : ILabTestService {

  private readonly ILabTestRepository _labTestRepository;
  private readonly ILoggerService<LabTestService> _loggerService;

  public LabTestService(ILabTestRepository labTestRepository, IDoctorRepository doctorRepository, ILabResultRepository labResultRepository, ILoggerService<LabTestService> loggerService) {
    _labTestRepository = labTestRepository;

    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      var labTest = await _labTestRepository.GetEntity(id);
      if (labTest != null) {
        result.Data = labTest.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = "LabTest not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting labTest";
    }
    return result;
  }

  public async Task<SaveLabTestVm> GetEntity(int id) {
    ServiceResult result = new();
    try {
      var labTest = await _labTestRepository.GetEntity(id);
      if (labTest != null) {
        return labTest.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = "LabTest not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting labTest";
    }
    return null;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from labTest in await _labTestRepository.GetAll()
                  select labTest.ConvertToVm();
      result.Data = query.ToList();
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while getting labTests";
    }
    return result;
  }
  public async Task<SaveLabTestVm> Save(SaveLabTestVm vm, string createdBy) {
    ServiceResult result = new();
    try {
      var isValidTest = ValidateLabTest.IsValidLabTest(vm);
      if (isValidTest.Success) {
        vm.CreatedBy = createdBy;
        LabTest labTest = vm.ConvertToSave();
        var newTest = await _labTestRepository.Save(labTest);
        return newTest.ConvertToSaveVm();
      } else {
        result.Success = false;
        result.Message = isValidTest.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while saving labTest";
    }
    return null;
  }

  public async Task Edit(SaveLabTestVm vm, string modifiedBy) {
    ServiceResult result = new();
    try {
      var isValidTest = ValidateLabTest.IsValidLabTest(vm);
      if (isValidTest.Success) {
        var labTestToUpdate = await _labTestRepository.GetEntity(vm.Id);
        if (labTestToUpdate != null) {
          labTestToUpdate.LastModifiedBy = modifiedBy;
          LabTest labTest = vm.ConvertToUpdate(labTestToUpdate);
          await _labTestRepository.Update(labTest);
        } else {
          result.Success = false;
          result.Message = "LabTest not found";
        }
      } else {
        result.Success = false;
        result.Message = isValidTest.Message;
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while editing labTest";
    }
  }
  public async Task Delete(int id) {
    ServiceResult result = new();
    try {
      LabTest labTestToDelete = await _labTestRepository.GetEntity(id);
      if (labTestToDelete != null) {
        await _labTestRepository.Delete(labTestToDelete);
      } else {
        result.Success = false;
        result.Message = "LabTest not found";
      }
    } catch (Exception ex) {
      _loggerService.LogError(ex.Message);
      result.Success = false;
      result.Message = "Error while deleting labTest";
    }
  }

}
