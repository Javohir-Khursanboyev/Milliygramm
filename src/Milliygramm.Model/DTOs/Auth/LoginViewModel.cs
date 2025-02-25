using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Model.DTOs.Auth;

public sealed class LoginViewModel
{
    public UserViewModel User { get; set; }
    public string Token { get; set; }
}
