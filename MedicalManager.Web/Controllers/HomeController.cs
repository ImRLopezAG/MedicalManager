using MedicalManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedicalManager.Web.Controllers {
  public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
      _logger = logger;
    }

    public IActionResult Index() {
      return View();
    }

    public IActionResult Admin() {
      return View();
    }
    public IActionResult Assistant() {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}