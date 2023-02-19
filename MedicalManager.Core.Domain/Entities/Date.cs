using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class Date : BaseEntity {
  public string Description { get; set; } = null!;
  public DateTime Day { get; set; }
  public DateTime Hour { get; set; }
  public string Status { get; set; } = null!;

  public int PatientId { get; set; }
  public int DoctorId { get; set; }
  public int? LabResultId { get; set; }

  public Patient Patient { get; set; } = null!;
  public Doctor Doctor { get; set; } = null!;
  public ICollection<LabResult>? LabResults { get; set; }

  public ICollection<LabTest>? LabTests { get; set; }
}
