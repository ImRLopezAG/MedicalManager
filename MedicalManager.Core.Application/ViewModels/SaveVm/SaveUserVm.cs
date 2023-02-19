using MedicalManager.Core.Application.ViewModels.SaveVm.core;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm;

public class SaveUserVm : BasePersonSaveVm {
  [Required(ErrorMessage = "User name is required")]
  [DataType(DataType.Text)]
  public string UserName { get; set; } = null!;
  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;

  [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
  [Required(ErrorMessage = "Confirm password is required")]
  [DataType(DataType.Password)]
  public string ConfirmPassword { get; set; } = null!;

  [Required(ErrorMessage = "Role is required")]
  public string Role { get; set; } = null!;

  public bool UserExists { get; set; }
}
