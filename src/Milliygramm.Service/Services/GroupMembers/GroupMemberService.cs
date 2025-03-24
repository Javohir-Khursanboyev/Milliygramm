using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.GroupMembers;
using Milliygramm.Service.Configurations;
using Milliygramm.Service.Exceptions;

namespace Milliygramm.Service.Services.GroupMembers;

public sealed class GroupMemberService(IMapper mapper,
    IUnitOfWork unitOfWork) : IGroupMemberService
{
    public async Task<GroupMemberVievModel> CreateAsync(GroupMemberCreateModel createModel)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(group => group.Id == createModel.GroupId && !group.IsDeleted)
            ?? throw new NotFoundException($"Group is not found this ID = {createModel.GroupId}");
       
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == createModel.MemberId && !user.IsDeleted)
            ?? throw new NotFoundException($"Member is not found this ID = {createModel.MemberId}");
       
        var existingGroupMember = await unitOfWork.GroupMembers.SelectAsync(gm => gm.GroupId == createModel.GroupId && gm.MemberId == createModel.MemberId);
        if (existingGroupMember is not null)
            throw new AlreadyExistException($"User with ID = {createModel.MemberId} is already a member of Group ID = {createModel.GroupId}");

        var createGroupMember = mapper.Map<GroupMember>(createModel);
        await unitOfWork.GroupMembers.InsertAsync(createGroupMember);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupMemberVievModel>(createGroupMember);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existGroupMember = await unitOfWork.GroupMembers.SelectAsync(groupMember => groupMember.Id == id)
            ?? throw new NotFoundException($"Group member is not found this ID = {id}");

        await unitOfWork.GroupMembers.DropAsync(existGroupMember);
        await unitOfWork.SaveAsync();
        return true;
    }
    public async Task<GroupMemberVievModel> GetByIdAsync(long id)
    {
        var existGroupMember = await unitOfWork.GroupMembers.SelectAsync(groupMember => groupMember.Id == id)
            ?? throw new NotFoundException($"Group member is not found this ID = {id}");

        return mapper.Map<GroupMemberVievModel>(existGroupMember);
    }
}
