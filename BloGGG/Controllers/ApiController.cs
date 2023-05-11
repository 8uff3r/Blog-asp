using Microsoft.AspNetCore.Mvc;

namespace BloGGG.Controllers;

public class ApiController : Controller
{
    // GET
    [HttpPost]
    public void Index()
    {
        Console.WriteLine("GOT 'Em");
    }
}