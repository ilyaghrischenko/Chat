using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class ChatController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}