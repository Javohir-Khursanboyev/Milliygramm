using Milliygramm.Model.DTOs.GroupDetails;

namespace Milliygramm.Service.Services.GroupDetails;

public interface IGroupDetailService
{
    Task<GroupDetailVievModel> CreateAsync(GroupDetailCreateModel createModel);
    Task<GroupDetailVievModel> UpdateAsync(long id, GroupDetailUpdateModel updateModel);
}
