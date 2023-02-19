using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Core.Application.ViewModels.SaveVm.core;

public class BaseCustomerSaveVm : BasePersonSaveVm {
  [Required(ErrorMessage = "Identification is required")]
  public string Identification { get; set; } = null!;
  public string? Photo { get; set; } = null!;

  [DataType(DataType.ImageUrl)]
  public IFormFile? File { get; set; } = null!;

  [DataType(DataType.PhoneNumber)]
  public string Phone { get; set; } = null!;
}
