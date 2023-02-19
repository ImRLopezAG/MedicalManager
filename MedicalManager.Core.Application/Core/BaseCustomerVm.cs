namespace MedicalManager.Core.Application.Core;

public class BaseCustomerVm : BasePersonVm {
  public string Identification { get; set; }
  public string Photo { get; set; }

  public string Phone { get; set; }
}
