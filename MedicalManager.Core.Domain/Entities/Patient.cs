using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class Patient : BaseCustomerEntity {
  public string? Address { get; set; }
  public DateTime BirthDate { get; set; }
  public bool Smoker { get; set; }
  public bool Allergic { get; set; }

  public ICollection<Date> Dates { get; set; }
  public ICollection<LabResult> LabResults { get; set; }
  public ICollection<LabTest> LabTests { get; set; }

}
