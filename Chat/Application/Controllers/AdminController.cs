using Application.Services.ControllerServices;
using DataBase.Context;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

public class AdminController(AdminControllerService adminControllerService, EntityListsRepository entityListsRepository)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    => View(await entityListsRepository.GetAllUsers());
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await adminControllerService.DeleteUser(id);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUserChats(int id)
    {
        await adminControllerService.DeleteUserChats(id);
        return RedirectToAction("Index");
    }
}