using UserManagementApp.Enums;

namespace UserManagementApp.ViewModels;

public class GetUserViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset RegisterDate { get; set; }
    public DateTimeOffset LastLoginDate { get; set; }
    public Status Status { get; set; }
}