using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Enums;
using UserManagementApp.Services;

namespace UserManagementApp.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Authorize(Policy = "UserIsUnblocked")]
    public async Task<IActionResult> Index()
    {
        ViewBag.ListEmployee = await _userService.Get();
        return View();
    }
    
    [HttpPost]
    [Authorize(Policy = "UserIsUnblocked")]
    public async Task<IActionResult> Index(IFormCollection formCollection, Operation operation)
    {
        Console.WriteLine("OPERATION: " + operation);
        switch (operation)
        {
            case Operation.Block:
                await _userService.Block(formCollection);
                break;
            case Operation.Delete:
                await _userService.Delete(formCollection);
                break;
            case Operation.Unblock:
                await _userService.Unblock(formCollection);
                break;
            default:
                return RedirectToAction("Index");
        }
        
        return RedirectToAction("Index");
    }
}