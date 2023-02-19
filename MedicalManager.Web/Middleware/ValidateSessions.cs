using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;

namespace MedicalManager.Web.Middleware;

public class ValidateSessions {
  private readonly IHttpContextAccessor _httpContextAccessor;

  public ValidateSessions(IHttpContextAccessor httpContextAccessor) {
    _httpContextAccessor = httpContextAccessor;
  }

  public bool HasUser() {
    UserVm userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserVm>("user");
    if (userViewModel == null)
      return false;

    return true;
  }

}
