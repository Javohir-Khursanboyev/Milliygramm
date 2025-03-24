using Milliygramm.Model.DTOs.GroupMembers;
using Milliygramm.Service.Configurations;

namespace Milliygramm.Service.Services.GroupMembers;

public interface IGroupMemberService
{
    Task<GroupMemberVievModel> CreateAsync(GroupMemberCreateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<GroupMemberVievModel> GetByIdAsync(long id);
}
