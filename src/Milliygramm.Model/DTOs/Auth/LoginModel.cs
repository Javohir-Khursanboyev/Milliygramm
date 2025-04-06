using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Auth;

public sealed class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}