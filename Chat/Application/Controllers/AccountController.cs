using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Services.ControllerServices.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Controllers;

public class AccountController(ILogger<AccountController> logger, IAccountControllerService accountControllerService)
    : Controller
{
    [HttpGet]
    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var user = await accountControllerService.LogInAsync(model);
            TempData["UserId"] = user.Id;
            return RedirectToAction("Index", "Chat");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while logging in.");
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            await accountControllerService.SignUpAsync(model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while signing up.");
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        return View("LogIn");
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}