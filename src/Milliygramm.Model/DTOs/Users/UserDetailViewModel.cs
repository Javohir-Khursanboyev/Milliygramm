using Milliygramm.Model.DTOs.Assets;

namespace Milliygramm.Model.DTOs.Users;

public sealed class UserDetailViewModel
{
    public long Id { get; set; }
    public string Bio { get; set; }
    public DateTime? DataOfBirth { get; set; }
    public string Location { get; set; }
    public AssetViewModel Picture { get; set; }
}