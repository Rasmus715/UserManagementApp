using System.Globalization;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Data;
using UserManagementApp.Enums;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Services;

public interface IUserService
{
    Task<List<GetUserViewModel>> Get();
    Task Block(IFormCollection formCollection);
    Task Unblock(IFormCollection formCollection);
    Task Delete(IFormCollection formCollection);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetUserViewModel>> Get()
    {
        return await _context.Users.Select(u => new GetUserViewModel
        {
            Id = u.Id,
            Name = u.UserName,
            Email = u.Email,
            RegisterDate = u.RegistrationDate,
            LastLoginDate = u.LastLoginDate,
            Status = u.Status
        }).ToListAsync();
    }

    public async Task Block(IFormCollection formCollection)
    {
        foreach (var id in formCollection["userCheckbox"].ToArray())
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (user != null) 
                user.Status = Status.Blocked;
            
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task Unblock(IFormCollection formCollection)
    {
        foreach (var id in formCollection["userCheckbox"].ToArray())
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (user != null) 
                user.Status = Status.Unblocked;
            
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task Delete(IFormCollection formCollection)
    {
        foreach (var id in formCollection["userCheckbox"].ToArray())
        {
            _context.Users.Remove((await _context.Users.FirstOrDefaultAsync(c => c.Id == id))!);

            await _context.SaveChangesAsync();
        }
    }
    
}