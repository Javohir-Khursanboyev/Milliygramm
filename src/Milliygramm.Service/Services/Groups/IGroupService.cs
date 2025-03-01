using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Groups;
using Milliygramm.Service.Configurations;

namespace Milliygramm.Service.Services.Groups;

public interface IGroupService
{
    Task<GroupViewModel> CreateAsync(GroupCreatModel creatModel);
    Task<GroupViewModel> UpdateAsync(long id, GroupUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<GroupViewModel> GetByIdAsync(long id);
    Task<IEnumerable<GroupViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
