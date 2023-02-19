using MedicalManager.Core.Domain.Core;

namespace MedicalManager.Core.Domain.Entities;

public class User : BasePersonEntity {
  public string UserName { get; set; }
  public string Password { get; set; }
  public string Role { get; set; }
}
