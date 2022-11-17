using Microsoft.AspNetCore.Identity;
using UserManagementApp.Enums;

namespace UserManagementApp.Models;

public class User : IdentityUser
{
    public DateTimeOffset RegistrationDate { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }
    public Status Status { get; set; }
}