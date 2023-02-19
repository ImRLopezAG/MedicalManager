using MedicalManager.Core.Application.Core;

namespace MedicalManager.Core.Application.ViewModels;

public class PatientVm : BaseCustomerVm {
  public string? Address { get; set; }
  public DateOnly BirthDate { get; set; }
  public string Smoker { get; set; } = null!;
  public string Allergic { get; set; } = null!;
}