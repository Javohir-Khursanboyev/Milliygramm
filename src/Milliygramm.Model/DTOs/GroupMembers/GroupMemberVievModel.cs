using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Groups;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Model.DTOs.GroupMembers;

public sealed class GroupMemberVievModel
{
    public long Id { get; set; }
    public long GroupId { get; set; }
    public long MemberId { get; set; }
    public UserViewModel Member { get; set; }
}
