using MedicalManager.Core.Application.Core;

namespace MedicalManager.Core.Application.ViewModels;

public class LabTestVm : BaseVM {
  public string Name { get; set; } = null!;
  public string Patient { get; set; } = null!;
  public string Doctor { get; set; } = null!;
}
