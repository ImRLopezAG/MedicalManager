using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class DateController : Controller {
    private readonly ValidateSessions _validateSession;
    private readonly IDateService _dateService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly ILabTestService _labTestService;

    public DateController(IDateService dateService, IPatientService patientService, IDoctorService doctorService, ILabTestService labTestService, ValidateSessions validateSession) {
      _dateService = dateService;
      _patientService = patientService;
      _doctorService = doctorService;
      _labTestService = labTestService;
      _validateSession = validateSession;
    }
    public async Task<IActionResult> Index() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View(await _dateService.GetAll().ContinueWith(x => x.Result.Data));
    }
    public async Task<IActionResult> AddTest(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View(await _dateService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTest(int id, int[] LabTestId) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        await _dateService.AddTest(id, LabTestId, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
    public async Task<IActionResult> Create() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      return View("Create", new SaveDateVm() {
        Doctors = ( List<DoctorVm> )await _doctorService.GetAll().ContinueWith(x => x.Result.Data),
        Patients = ( List<PatientVm> )await _patientService.GetAll().ContinueWith(x => x.Result.Data),
        Day = DateTime.Now.AddDays(1),
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaveDateVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        if (!ModelState.IsValid)
          return View("Create");

        await _dateService.Save(model, HttpContext.Session.Get<UserVm>("user").UserName);
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

      return View("Edit", await _dateService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SaveDateVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Assistant")
        return RedirectToRoute(new { controller = "Home", action = "Admin" });

      try {
        if (!ModelState.IsValid)
          return View("Edit", model);

        await _dateService.Edit(model, HttpContext.Session.Get<UserVm>("user").UserName);
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
        await _dateService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
