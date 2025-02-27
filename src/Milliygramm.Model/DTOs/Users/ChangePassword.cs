namespace Milliygramm.Model.DTOs.Users;

public sealed class ChangePassword
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}