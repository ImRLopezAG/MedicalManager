using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class LabTestController : Controller {
    private readonly ValidateSessions _validateSession;
    private readonly ILabTestService _labTestService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public LabTestController(ILabTestService labTestService, IPatientService patientService, IDoctorService doctorService, ValidateSessions validateSession) {
      _labTestService = labTestService;
      _patientService = patientService;
      _doctorService = doctorService;
      _validateSession = validateSession;
    }
    public async Task<IActionResult> Index() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      return View(await _labTestService.GetAll().ContinueWith(x => x.Result.Data));
    }

    public async Task<IActionResult> Create() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      ViewBag.Patients = await _patientService.GetAll().ContinueWith(x => x.Result.Data);
      ViewBag.Doctors = await _doctorService.GetAll().ContinueWith(x => x.Result.Data);
      return View("Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaveLabTestVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Create");
        await _labTestService.Save(model, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }

    public async Task<IActionResult> Edit(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      ViewBag.Patients = await _patientService.GetAll().ContinueWith(x => x.Result.Data);
      ViewBag.Doctors = await _doctorService.GetAll().ContinueWith(x => x.Result.Data);
      return View(await _labTestService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SaveLabTestVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Edit", model);

        await _labTestService.Edit(model, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(Index));
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
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        await _labTestService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
