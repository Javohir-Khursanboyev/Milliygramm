using Milliygramm.Domain.Entities;

namespace Milliygramm.Model.DTOs.GroupMembers;

public sealed class GroupMemberCreateModel
{
    public long GroupId { get; set; }
    public long MemberId { get; set; }
}
