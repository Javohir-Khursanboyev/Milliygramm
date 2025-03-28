using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Service.Configurations;

namespace Milliygramm.Service.Services.Chats;

public interface IChatService
{
    Task<ChatVievModel> CreatAsync(ChatCreateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<ChatVievModel> GetByIdAsync(long id);

    Task<ChatVievModel> UpdateAsync(long id, ChatUpdateModel updateModel);
    Task<IEnumerable<ChatVievModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
