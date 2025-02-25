namespace Milliygramm.Model.DTOs.Users;

public sealed class UserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserDetailViewModel UserDetail { get; set; }
}