using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class LabResult : BaseEntity {
  public string? Result { get; set; }
  public string Status { get; set; }

  public int LabTestId { get; set; }
  public int DateId { get; set; }

  public LabTest LabTest { get; set; }
  public Date Date { get; set; }
}