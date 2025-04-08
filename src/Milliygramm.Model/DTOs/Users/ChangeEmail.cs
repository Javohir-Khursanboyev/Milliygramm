using System.ComponentModel.DataAnnotations;

namespace Milliygramm.Model.DTOs.Users;

public sealed class ChangeEmail
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}