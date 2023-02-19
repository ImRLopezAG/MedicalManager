using MedicalManager.Core.Application.ViewModels.SaveVm.core;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm;

public class SaveDateVm : BaseSaveVm {
  [Required(ErrorMessage = "Description is required")]
  public string Description { get; set; } = null!;
  [Required(ErrorMessage = "Day is required")]
  [DataType(DataType.Date)]
  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
  public DateTime Day { get; set; }
  [Required(ErrorMessage = "Hour is required")]
  [DataType(DataType.Time)]
  [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
  public DateTime Hour { get; set; }
  public string? Status { get; set; } = null!;
  [Required(ErrorMessage = "Patient is required")]
  public int PatientId { get; set; }
  [Required(ErrorMessage = "Doctor is required")]
  public int DoctorId { get; set; }

  public int? LabTestId { get; set; }
  public List<PatientVm> Patients { get; set; } = new();
  public List<DoctorVm> Doctors { get; set; } = new();
  public List<LabTestVm> LabTests { get; set; } = new();
}