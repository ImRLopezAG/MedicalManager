using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Helpers;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class PatientController : Controller {
    private readonly ValidateSessions _validateSession;

    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService, ValidateSessions validateSession) {
      _patientService = patientService;
      _validateSession = validateSession;
    }
    public async Task<IActionResult> Index() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View(await _patientService.GetAll().ContinueWith(x => x.Result.Data));
    }

    public IActionResult Create() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View("Create", new SavePatientVm {
        BirthDate = DateTime.Parse("2000-01-01"),
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SavePatientVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        if (!ModelState.IsValid)
          return View("Create");

        SavePatientVm patient = await _patientService.Save(model, HttpContext.Session.Get<UserVm>("user").UserName);
        if (patient.Id != 0 && patient != null)
          patient.Photo = ManageFile.Upload(model.File, patient.Id, "Patient");
        await _patientService.Edit(patient, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }

    public async Task<IActionResult> Edit(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View(await _patientService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SavePatientVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        if (!ModelState.IsValid)
          return View("Edit", model);

        var patient = await _patientService.GetEntity(model.Id);
        model.Photo = ManageFile.Upload(model.File, model.Id, "Patient", true, patient.Photo);
        await _patientService.Edit(model, HttpContext.Session.Get<UserVm>("user").UserName);
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
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });


      try {
        await _patientService.Delete(id);
        ManageFile.Delete(id, "Patient");
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
