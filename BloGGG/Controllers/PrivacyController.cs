using Microsoft.AspNetCore.Mvc;


namespace BloGGG.Controllers;

public class PrivacyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Privacy.cshtml");
    }
}