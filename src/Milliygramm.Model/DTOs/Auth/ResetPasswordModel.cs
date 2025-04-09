using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Auth;

public sealed class ResetPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public string NewPassword { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}