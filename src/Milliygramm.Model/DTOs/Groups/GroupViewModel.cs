using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Model.DTOs.GroupDetails;

namespace Milliygramm.Model.DTOs.Groups;

public sealed class GroupViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }            
    public ChatVievModel Chat { get; set; }
    public GroupDetailVievModel GroupDetail { get; set; }
}
