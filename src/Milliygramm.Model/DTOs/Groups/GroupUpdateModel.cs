using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Model.DTOs.GroupDetails;

namespace Milliygramm.Model.DTOs.Groups;

public sealed class GroupUpdateModel
{
    public string Name { get; set; }
    public GroupDetailUpdateModel GroupDetailUpdateModel { get; set; }
}
