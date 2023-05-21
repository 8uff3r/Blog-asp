using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BloGGG.Controllers;

[AllowAnonymous]
public class PrivacyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Privacy.cshtml");
    }
}