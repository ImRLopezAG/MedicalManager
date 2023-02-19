using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Helpers;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class DoctorController : Controller {
    private readonly ValidateSessions _validateSession;
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService, ValidateSessions validateSession) {
      _doctorService = doctorService;
      _validateSession = validateSession;
    }
    public async Task<IActionResult> Index() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      return View(await _doctorService.GetAll().ContinueWith(x => x.Result.Data));
    }
    public IActionResult Create() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      return View();
    }

    // POST: DoctorController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaveDoctorVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Create", model);
        SaveDoctorVm doctor = await _doctorService.Save(model, HttpContext.Session.Get<UserVm>("user").UserName);
        if (doctor.Id != 0 && doctor != null)
          doctor.Photo = ManageFile.Upload(model.File, doctor.Id, "Doctor");
        await _doctorService.Edit(doctor, HttpContext.Session.Get<UserVm>("user").UserName);
        return RedirectToAction(nameof(Index));
      } catch {
        return View("Create");
      }
    }
    public async Task<IActionResult> Edit(int id) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      return View(await _doctorService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SaveDoctorVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Edit", model);

        var doctor = await _doctorService.GetEntity(model.Id);
        model.Photo = ManageFile.Upload(model.File, model.Id, "Doctor", true, doctor.Photo);
        await _doctorService.Edit(model, HttpContext.Session.Get<UserVm>("user").UserName);
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
        await _doctorService.Delete(id);
        ManageFile.Delete(id, "Doctor");
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
