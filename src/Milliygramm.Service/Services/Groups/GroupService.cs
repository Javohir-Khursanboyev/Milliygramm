using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;
using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Model.DTOs.Groups;
using Milliygramm.Service.Configurations;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Helpers;
using Milliygramm.Service.Services.Chats;
using Milliygramm.Service.Validators.Groups;

namespace Milliygramm.Service.Services.Groups;

public sealed class GroupService(IMapper mapper,
    IUnitOfWork unitOfWork,
    GroupCreatModelValidator creatModelValidator,
    GroupUpdateModelValidator updateModelValidator,
    IChatService chatService) : IGroupService
{
    public async Task<GroupViewModel> CreateAsync(GroupCreatModel creatModel)
    {
        await unitOfWork.BeginTransactionAsync();
        await creatModelValidator.EnsureValidatedAsync(creatModel);
        var existGroup = await unitOfWork.Groups.SelectAsync(group => group.Name == creatModel.Name)
            ?? throw new AlreadyExistException($"There is a group with this name = {creatModel.Name}, please choose another name to avoid confusion");

        var group = mapper.Map<Group>(existGroup);
        var createdGroup = await unitOfWork.Groups.InsertAsync(group);
        await unitOfWork.SaveAsync();
        var chat = new ChatCreateModel
        {
            Name = creatModel.Name,
            ChatType = ChatType.Group,
            OwnerId = HttpContextHelper.UserId,
            GroupId = createdGroup.Id,
        };

        await chatService.CreatAsync(chat);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();
        return mapper.Map<GroupViewModel>(group);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(group => group.Id == id && !group.IsDeleted)
            ?? throw new NotFoundException($"Group is not found this id = {id}");

        existGroup.DeletedAt = DateTime.UtcNow;
        await unitOfWork.Groups.DeleteAsync(existGroup);
        await unitOfWork.SaveAsync();
        return true;
    }
    public async Task<GroupViewModel> UpdateAsync(long id, GroupUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var existGroup = await unitOfWork.Groups.SelectAsync(group => group.Id == id && group.IsDeleted)
            ?? throw new NotFoundException($"Group is not found this id = {id}");

        if (existGroup.Name == updateModel.Name)
            throw new AlreadyExistException($"There is a group with this name = {updateModel.Name}, please choose another name to avoid confusion");

        existGroup.UpdatedAt = DateTime.UtcNow;
        existGroup.Name = updateModel.Name;
        await unitOfWork.Groups.UpdateAsync(existGroup);
        await unitOfWork.SaveAsync();
        return mapper.Map<GroupViewModel>(existGroup);
    }
    public async Task<GroupViewModel> GetByIdAsync(long id)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(group => group.Id == id && group.IsDeleted, includes: ["GroupDetail", "Members"])
            ?? throw new NotFoundException($"Group is not found this id = {id}");

        return mapper.Map<GroupViewModel>(existGroup);
    }

    public async Task<IEnumerable<GroupViewModel>> GetAllAsync(PaginationParams @params,
        Filter filter,
        string search = null)
    {
        var groups = unitOfWork.Groups.SelectAsQueryable(expression: group => !group.IsDeleted, includes: ["GroupDetail", "Members"]);
        if (!string.IsNullOrEmpty(search))
            groups = groups.Where(chat => chat.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        await groups.ToPaginateAsQueryable(@params).ToListAsync();

        return mapper.Map<IEnumerable<GroupViewModel>>(groups);
    }
}
