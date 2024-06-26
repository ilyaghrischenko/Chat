using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using DataBase.CRUD.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserService _userService = new();

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

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
        
        if(model.Login == "Admin" && model.Password == "123456")
        {
            return RedirectToAction("Index", "Admin");
        }

        User user;
        try
        {
            user = await _userService.GetAsync(model.Login, model.Password);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in.");
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        TempData["UserId"] = user.Id;
        return RedirectToAction("Index", "Chat");
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
            PasswordHasher<SignUpViewModel> _passwordHasher = new();
            await _userService.AddAsync(new User
            {
                Login = model.Login,
                Email = model.Email,
                Password = _passwordHasher.HashPassword(model, model.Password)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while signing up.");
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