using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Enums;
using UserManagementApp.Models;
using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly SignInManager<User> _signInManager;

    public AccountController(SignInManager<User> signInManager, IAccountService accountService)
    {
        _signInManager = signInManager;
        _accountService = accountService;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _signInManager.SignInAsync(await _accountService.Register(model), true);
                return RedirectToAction("Index", "User");
            }
            catch (DuplicateNameException)
            {
                ModelState.AddModelError("", "User with such email already exists");
                return View(model);
            }
        }
        
        ModelState.AddModelError("", "Incorrect Email and/or Password");
        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        try
        {
            var user = await _accountService.Login(model);
            if (user.Status == Status.Blocked)
            {
                ModelState.AddModelError("", "You've been locked out of the app");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if(result.Succeeded) 
                return RedirectToAction("Index", "User");
            ModelState.AddModelError("", "Incorrect Email and/or Password");
            return View(model);
        }
        catch (KeyNotFoundException)
        {
            ModelState.AddModelError("", "Incorrect Email and/or Password");
            return View(model);
        }
    }
    
    
    public async Task<IActionResult> Logout()
    {
        
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}