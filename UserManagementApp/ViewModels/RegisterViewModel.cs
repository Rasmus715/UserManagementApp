using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is not present")]
    public string Name { get; set; } 
    
    [Required(ErrorMessage = "Email is not present")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Please, enter valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is not present")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}