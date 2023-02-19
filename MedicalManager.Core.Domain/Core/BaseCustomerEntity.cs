namespace MedicalManager.Core.Domain.Core;

public class BaseCustomerEntity : BasePersonEntity {
  public string Identification { get; set; } = null!;
  public string? Photo { get; set; } = null!;
  public string Phone { get; set; } = null!;
}
