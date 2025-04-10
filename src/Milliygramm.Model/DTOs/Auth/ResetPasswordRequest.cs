using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Auth;

public sealed class ResetPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}