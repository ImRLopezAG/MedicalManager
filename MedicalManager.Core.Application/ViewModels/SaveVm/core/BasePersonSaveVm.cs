using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm.core;

public class BasePersonSaveVm : BaseSaveVm {
  [Required(ErrorMessage = "First name is required")]
  public string FirstName { get; set; } = null!;
  [Required(ErrorMessage = "Last name is required")]
  public string LastName { get; set; } = null!;
  [Required(ErrorMessage = "Email is required")]
  public string Email { get; set; } = null!;
}
