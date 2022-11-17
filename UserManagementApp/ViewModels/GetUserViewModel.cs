using UserManagementApp.Enums;

namespace UserManagementApp.ViewModels;

public class GetUserViewModel
{
    public string Id { get; init; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTimeOffset RegisterDate { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }
    public Status Status { get; set; }
}