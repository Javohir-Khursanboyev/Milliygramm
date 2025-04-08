namespace Milliygramm.Model.DTOs.Users;

public sealed class UserUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public UserDetailUpdateModel UserDetail { get; set; }
}