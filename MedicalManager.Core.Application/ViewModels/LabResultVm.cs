using MedicalManager.Core.Application.Core;

namespace MedicalManager.Core.Application.ViewModels;

public class LabResultVm : BaseVM {
  public string Status { get; set; } = null!;
  public string Result { get; set; } = null!;
  public string LabTest { get; set; } = null!;
  public string Patient { get; set; } = null!;
  public int DateId { get; set; }
  public string DNI { get; set; } = null!;
}
