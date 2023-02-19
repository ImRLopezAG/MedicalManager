using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class LabResultController : Controller {
    private readonly ValidateSessions _validateSession;
    private readonly ILabResultService _labResultService;

    public LabResultController(ILabResultService labResultService, ValidateSessions validateSession) {
      _labResultService = labResultService;
      _validateSession = validateSession;
    }
    public async Task<IActionResult> Index() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      ViewBag.LabResults = await _labResultService.GetAll().ContinueWith(x => x.Result.Data);
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterByDNI(string DNI) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      var labResult = await _labResultService.GetByDNI(DNI);
      ViewBag.LabResults = labResult.Data;
      ViewBag.Message = labResult.Message;
      ViewBag.Success = labResult.Success; ;
      return View("DNI");
    }

    public async Task<IActionResult> GetByDate(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      var labResult = await _labResultService.GetByDate(id).ContinueWith(x => x.Result.Data);
      ViewBag.LabResults = labResult;
      ViewBag.Date = id;
      return View("Date");
    }

    public async Task<IActionResult> GetComplete(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      var labResult = await _labResultService.GetByStatus("Completed", id).ContinueWith(x => x.Result.Data);
      ViewBag.LabResults = labResult;
      return View("Complete");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int dateId) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      await _labResultService.ChangeStatus(dateId, HttpContext.Session.Get<UserVm>("user").UserName);
      var labResult = await _labResultService.GetAll().ContinueWith(x => x.Result.Data);
      ViewBag.LabResults = labResult;
      return RedirectToRoute(new { controller = "Date", action = "Index" });
    }

    public async Task<IActionResult> GetPending() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      var labResult = await _labResultService.GetByStatus("Pending").ContinueWith(x => x.Result.Data);
      ViewBag.LabResults = labResult;
      return View("Index");
    }

    public async Task<IActionResult> Edit(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      var labResult = await _labResultService.GetEntity(id);
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SaveLabResultVm labResult) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        await _labResultService.Edit(labResult, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(GetPending));
      } catch {
        return View();
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        await _labResultService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
