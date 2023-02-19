using MedicalManager.Core.Application.ViewModels.SaveVm.core;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm;

public class SaveLabResultVm : BaseSaveVm {
  [Required(ErrorMessage = "The result is required")]
  [StringLength(50, ErrorMessage = "The result can't be longer than 50 characters")]
  [DataType(DataType.Text)]
  public string Result { get; set; } = null!;
  public string? Status { get; set; } = null!;
}
