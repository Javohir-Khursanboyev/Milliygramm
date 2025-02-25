namespace Milliygramm.Model.DTOs.Users;

public sealed class UserDetailUpdateModel
{
    public string Bio { get; set; }
    public DateTime? DataOfBirth { get; set; }
    public string Location { get; set; }
}
