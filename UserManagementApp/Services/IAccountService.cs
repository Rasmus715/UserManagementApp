using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Data;
using UserManagementApp.Enums;
using UserManagementApp.Models;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Services;

public interface IAccountService
{
    Task<User> Register(RegisterViewModel model);
    Task<User> Login(LoginViewModel model);
}

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;


    public AccountService(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<User> Register(RegisterViewModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user != null)
        { 
            throw new DuplicateNameException("credentials");
        }
            
        user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Name);
        if (user != null)
        {
            //UseName should be unique too. Identity limitations
            throw new DuplicateNameException("name");
        }
        user = new User
        {
            UserName = model.Name,
            Email = model.Email,
            RegistrationDate = DateTime.Now,
            LastLoginDate = DateTime.Now,
            Status = Status.Unblocked
        };
        
        await _userManager.CreateAsync(user, model.Password);
        
        return user;
    }

    public async Task<User> Login(LoginViewModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null) 
            throw new KeyNotFoundException();
        
        user.LastLoginDate = DateTimeOffset.Now;
        await _userManager.UpdateAsync(user);
        return user;
    }
}