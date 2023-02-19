using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Application.ViewModels.SaveVm;
using MedicalManager.Web.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MedicalManager.Web.Controllers {
  public class UserController : Controller {
    private readonly ValidateSessions _validateSession;
    private readonly IUserService _userService;


    public UserController(IUserService userService, ValidateSessions validateSession) {
      _userService = userService;
      _validateSession = validateSession;
    }
    public IActionResult Index() {
      if (_validateSession.HasUser())
        if (HttpContext.Session.Get<UserVm>("user").Role == "Admin")
          return RedirectToRoute(new { controller = "User", action = "Users" });
        else
          return RedirectToRoute(new { controller = "LabResult", action = "Index" });

      return View("Login");
    }
    public async Task<IActionResult> Users() {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });
      return View("Index", await _userService.GetAll().ContinueWith(x => x.Result.Data));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model) {
      try {
        if (!ModelState.IsValid)
          return View(model);
        UserVm user = await _userService.Login(model);
        if (user != null) {
          HttpContext.Session.Set<UserVm>("user", user);
          if (user.Role == "Admin")
            return RedirectToAction(nameof(Users));
          else
            return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        } else {
          ModelState.AddModelError("", "Invalid username or password");
          return View();
        }
      } catch {
        return View();
      }
    }

    public IActionResult LogOut() {
      HttpContext.Session.Remove("user");
      return RedirectToAction(nameof(Index));
    }


    public IActionResult Create() {
      if (_validateSession.HasUser())
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
          return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      return View("Create", new SaveUserVm { UserExists = false });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SaveUserVm model) {
      if (_validateSession.HasUser())
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
          return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Create");

        if (_userService.UserExists(model.UserName).Result)
          return View("Create", new SaveUserVm { UserExists = true });

        await _userService.Save(model, _validateSession.HasUser() ? HttpContext.Session.Get<UserVm>("user").UserName : "New User");
        return RedirectToAction(nameof(Users));

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

      return View(await _userService.GetEntity(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SaveUserVm model) {
      if (!_validateSession.HasUser())
        return RedirectToRoute(new { controller = "User", action = "Index" });
      else
        if (HttpContext.Session.Get<UserVm>("user").Role != "Admin")
        return RedirectToRoute(new { controller = "Home", action = "Assistant" });

      try {
        if (!ModelState.IsValid)
          return View("Edit", model);

        var user = await _userService.GetEntity(model.Id);
        if (user.UserName != model.UserName)
          if (_userService.UserExists(model.UserName).Result)
            return View("Edit", new SaveUserVm { UserExists = true });

        await _userService.Edit(model, HttpContext.Session.Get<UserVm>("user").UserName);

        if (user.Role != model.Role)
          if (HttpContext.Session.Get<UserVm>("user").Id == model.Id)
            HttpContext.Session.Remove("user");

        return RedirectToAction(nameof(Users));
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
        await _userService.Delete(id);
        if (HttpContext.Session.Get<UserVm>("user").Id == id)
          HttpContext.Session.Remove("user");
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
