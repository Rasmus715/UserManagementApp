using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is not present")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please, enter valid email address")]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "Name is not present")]
    public string Password { get; init; } = null!;
}