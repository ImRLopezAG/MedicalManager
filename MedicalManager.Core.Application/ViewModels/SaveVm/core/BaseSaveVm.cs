namespace MedicalManager.Core.Application.ViewModels.SaveVm.core;

public class BaseSaveVm {
  public virtual int Id { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? LastModifiedBy { get; set; }
  public DateTime? LastModifiedAt { get; set; }
}
