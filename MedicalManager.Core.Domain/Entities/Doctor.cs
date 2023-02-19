using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class Doctor : BaseCustomerEntity {
  public ICollection<Date> Dates { get; set; }
  public ICollection<LabTest> LabTests { get; set; }
}
