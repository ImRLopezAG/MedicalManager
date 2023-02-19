using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class LabTest : BaseEntity {
  public string Name { get; set; }

  public int? DateId { get; set; }

  public Date? Date { get; set; }

  public ICollection<LabResult> LabResults { get; set; }
}
