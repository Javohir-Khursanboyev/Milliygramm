using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Service.Configurations;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;

namespace Milliygramm.Service.Services.Chats;

public sealed class ChatService(IMapper mapper, IUnitOfWork unitOfWork) : IChatService
{
    public async Task<ChatVievModel> CreatAsync(ChatCreateModel createModel)
    {
        var chat = mapper.Map<Chat>(createModel);
        await unitOfWork.Chats.InsertAsync(chat);
        await unitOfWork.SaveAsync();
        return mapper.Map<ChatVievModel>(chat);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(chat => chat.Id == id)
            ?? throw new NotFoundException($"Chat is not found this id = {id}");

        existChat.DeletedAt = DateTime.UtcNow;
        await unitOfWork.Chats.DeleteAsync(existChat);
        return await unitOfWork.SaveAsync(); ;
    }
    public async Task<ChatVievModel> GetByIdAsync(long id)
    {   
        var existChat = await unitOfWork.Chats.SelectAsync(chat => chat.Id == id && !chat.IsDeleted, includes: ["Group", "Participant", "Messages"])
            ?? throw new NotFoundException($"Chat is not found this id = {id}");

        return mapper.Map<ChatVievModel>(existChat);
    }
    public async Task<IEnumerable<ChatVievModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var chats = unitOfWork.Chats.SelectAsQueryable(expression: chat => !chat.IsDeleted, includes: ["Group", "Participant", "Messages"], isTracked: false).OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            chats = chats.Where(chat =>chat.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        await chats.ToPaginateAsQueryable(@params).ToListAsync();
        return  mapper.Map<IEnumerable<ChatVievModel>>(chats);
    }

}
