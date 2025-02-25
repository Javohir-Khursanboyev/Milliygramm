using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class GroupMember : Auditable
{
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public long MemberId { get; set; }
    public User Member { get; set; }
}
