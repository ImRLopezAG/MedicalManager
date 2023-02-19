using MedicalManager.Core.Application.Core;

namespace MedicalManager.Core.Application.ViewModels;

public class UserVm : BasePersonVm {
  public string UserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Role { get; set; } = null!;
}
