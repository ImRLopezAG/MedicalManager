using MedicalManager.Core.Application.Core;

namespace MedicalManager.Core.Application.ViewModels;

public class DateVm : BaseVM {
  public string Description { get; set; } = null!;
  public DateOnly Day { get; set; }
  public TimeOnly Hour { get; set; }
  public string Status { get; set; } = null!;

  public string Patient { get; set; } = null!;
  public string Doctor { get; set; } = null!;
}
