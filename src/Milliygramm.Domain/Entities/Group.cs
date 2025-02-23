using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public class Group : Auditable 
{
    public string Name { get; set; }
    public GroupDetail GroupDetail { get; set; }
    public IEnumerable<GroupMember> GroupMembers { get; set; }
}
