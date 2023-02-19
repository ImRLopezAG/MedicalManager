using MedicalManager.Core.Application.ViewModels.SaveVm.core;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm;

public class SavePatientVm : BaseCustomerSaveVm {
  public string? Address { get; set; }
  [Required(ErrorMessage = "Birth date is required")]
  [DataType(DataType.Date)]
  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
  public DateTime BirthDate { get; set; }
  [Required(ErrorMessage = "Smoker is required")]
  public bool Smoker { get; set; }
  [Required(ErrorMessage = "Allergic is required")]
  public bool Allergic { get; set; }
}
