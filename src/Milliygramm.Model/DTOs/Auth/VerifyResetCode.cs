using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Auth;

public sealed class VerifyResetCode
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Code { get; set; }
}
