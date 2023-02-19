using MedicalManager.Core.Application.ViewModels.SaveVm.core;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm;

public class SaveLabTestVm : BaseSaveVm {
  [Required(ErrorMessage = "Name is required")]
  public string Name { get; set; } = null!;
}
