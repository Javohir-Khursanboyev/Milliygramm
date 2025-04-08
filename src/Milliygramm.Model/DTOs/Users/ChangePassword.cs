using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Users;

public sealed class ChangePassword
{
    [Required]
    public string Password { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}